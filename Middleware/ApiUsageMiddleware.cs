using System.Diagnostics;
using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Middleware;

/// <summary>
/// Middleware for logging API usage statistics.
/// Tracks all requests to /api/* endpoints.
/// </summary>
public class ApiUsageMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiUsageMiddleware> _logger;

    public ApiUsageMiddleware(RequestDelegate next, ILogger<ApiUsageMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        // Only log API requests
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        // Skip swagger and culture endpoints
        if (context.Request.Path.StartsWithSegments("/api/Culture") ||
            context.Request.Path.Value?.Contains("swagger") == true)
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            try
            {
                await LogApiUsageAsync(context, dbContext, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log API usage for {Path}", context.Request.Path);
            }
        }
    }

    private async Task LogApiUsageAsync(HttpContext context, ApplicationDbContext dbContext, long responseTimeMs)
    {
        // Try to get API key from header or query string
        var apiKeyValue = context.Request.Headers["X-Api-Key"].FirstOrDefault()
                          ?? context.Request.Query["apiKey"].FirstOrDefault();

        long? apiKeyId = null;

        if (!string.IsNullOrEmpty(apiKeyValue))
        {
            var apiKey = await dbContext.ApiKeys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Key == apiKeyValue && k.Enabled);
            apiKeyId = apiKey?.Id;
        }

        // If no API key, try to get a default/system API key or create usage without it
        if (!apiKeyId.HasValue)
        {
            // Get or create a system API key for anonymous requests
            var systemKey = await dbContext.ApiKeys
                .FirstOrDefaultAsync(k => k.Key == "SYSTEM_ANONYMOUS");

            if (systemKey == null)
            {
                // Find admin user for system key
                var adminUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "admin@legislativeenums.cz");
                if (adminUser != null)
                {
                    systemKey = new ApiKey
                    {
                        Key = "SYSTEM_ANONYMOUS",
                        Name = "Anonymous API Usage Tracking",
                        UserId = adminUser.Id,
                        Enabled = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    dbContext.ApiKeys.Add(systemKey);
                    await dbContext.SaveChangesAsync();
                }
            }

            apiKeyId = systemKey?.Id;
        }

        if (!apiKeyId.HasValue)
        {
            _logger.LogWarning("Cannot log API usage - no API key available");
            return;
        }

        // Determine response format from response Content-Type header
        var contentType = context.Response.ContentType ?? "";
        var responseFormat = "JSON"; // Default

        if (contentType.Contains("xml", StringComparison.OrdinalIgnoreCase))
        {
            responseFormat = "XML";
        }
        else if (contentType.Contains("json", StringComparison.OrdinalIgnoreCase))
        {
            responseFormat = "JSON";
        }

        var usage = new ApiUsage
        {
            ApiKeyId = apiKeyId.Value,
            Endpoint = context.Request.Path.Value ?? "/api/unknown",
            Timestamp = DateTime.UtcNow,
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers["User-Agent"].FirstOrDefault(),
            ResponseStatus = context.Response.StatusCode,
            ResponseTimeMs = responseTimeMs,
            ResponseFormat = responseFormat
        };

        dbContext.ApiUsages.Add(usage);
        await dbContext.SaveChangesAsync();
    }
}

/// <summary>
/// Extension methods for registering ApiUsageMiddleware.
/// </summary>
public static class ApiUsageMiddlewareExtensions
{
    public static IApplicationBuilder UseApiUsageLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiUsageMiddleware>();
    }
}

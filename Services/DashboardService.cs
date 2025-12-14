using LegislativeEnumsNew.Data;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Services;

/// <summary>
/// Service for providing administrative dashboard statistics and analytics.
/// </summary>
public class DashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStats> GetDashboardStatsAsync()
    {
        var now = DateTime.UtcNow;
        var todayStart = now.Date;
        var weekStart = todayStart.AddDays(-7);
        var monthStart = todayStart.AddDays(-30);

        var stats = new DashboardStats
        {
            TotalUsers = await _context.Users.CountAsync(),
            TotalApiKeys = await _context.ApiKeys.CountAsync(),
            ActiveApiKeys = await _context.ApiKeys.CountAsync(k => k.Enabled),
            TotalCodelists = await _context.CodelistRegistry.CountAsync(),
            TotalAuditRecords = await _context.AuditLogs.CountAsync(),

            // API usage statistics
            RequestsToday = await _context.ApiUsages.CountAsync(u => u.Timestamp >= todayStart),
            RequestsWeek = await _context.ApiUsages.CountAsync(u => u.Timestamp >= weekStart),
            RequestsMonth = await _context.ApiUsages.CountAsync(u => u.Timestamp >= monthStart),
            RequestsAllTime = await _context.ApiUsages.CountAsync(),

            // Format statistics
            JsonRequestCount = await _context.ApiUsages.CountAsync(u => u.ResponseFormat == "JSON"),
            XmlRequestCount = await _context.ApiUsages.CountAsync(u => u.ResponseFormat == "XML"),

            // Top endpoints (last 30 days)
            TopEndpoints = await GetTopEndpointsAsync(monthStart, 10),

            // Top users (last 30 days)
            TopUsers = await GetTopUsersAsync(monthStart, 10),

            // Daily stats (last 30 days)
            DailyStats = await GetDailyStatsAsync(30),

            // Recent requests
            RecentRequests = await GetRecentRequestsAsync(50),

            // Recent audit logs
            RecentAuditLogs = await _context.AuditLogs
                .OrderByDescending(a => a.ChangedAt)
                .Take(10)
                .ToListAsync(),

            // Recent users
            RecentUsers = await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Take(5)
                .ToListAsync()
        };

        return stats;
    }

    private async Task<List<EndpointStats>> GetTopEndpointsAsync(DateTime since, int limit)
    {
        return await _context.ApiUsages
            .Where(u => u.Timestamp >= since)
            .GroupBy(u => u.Endpoint)
            .Select(g => new EndpointStats
            {
                Endpoint = g.Key,
                RequestCount = g.Count(),
                AvgResponseTimeMs = g.Average(x => x.ResponseTimeMs)
            })
            .OrderByDescending(e => e.RequestCount)
            .Take(limit)
            .ToListAsync();
    }

    private async Task<List<UserApiStats>> GetTopUsersAsync(DateTime since, int limit)
    {
        return await _context.ApiUsages
            .Where(u => u.Timestamp >= since)
            .Include(u => u.ApiKey)
            .ThenInclude(k => k.User)
            .GroupBy(u => new { u.ApiKey.User!.UserName })
            .Select(g => new UserApiStats
            {
                Username = g.Key.UserName ?? "Anonymous",
                RequestCount = g.Count()
            })
            .OrderByDescending(u => u.RequestCount)
            .Take(limit)
            .ToListAsync();
    }

    private async Task<Dictionary<string, long>> GetDailyStatsAsync(int days)
    {
        var startDate = DateTime.UtcNow.Date.AddDays(-(days - 1));
        var result = new Dictionary<string, long>();

        // Initialize all days with 0
        for (int i = 0; i < days; i++)
        {
            var date = startDate.AddDays(i);
            result[date.ToString("dd.MM")] = 0;
        }

        // Get actual counts
        var dailyCounts = await _context.ApiUsages
            .Where(u => u.Timestamp >= startDate)
            .GroupBy(u => u.Timestamp.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        foreach (var dc in dailyCounts)
        {
            var key = dc.Date.ToString("dd.MM");
            if (result.ContainsKey(key))
            {
                result[key] = dc.Count;
            }
        }

        return result;
    }

    private async Task<List<RecentRequest>> GetRecentRequestsAsync(int limit)
    {
        return await _context.ApiUsages
            .Include(u => u.ApiKey)
            .ThenInclude(k => k.User)
            .OrderByDescending(u => u.Timestamp)
            .Take(limit)
            .Select(u => new RecentRequest
            {
                Timestamp = u.Timestamp,
                Endpoint = u.Endpoint,
                Username = u.ApiKey.User != null ? u.ApiKey.User.UserName ?? "Anonymous" : "Anonymous",
                ResponseStatus = u.ResponseStatus,
                ResponseTimeMs = u.ResponseTimeMs,
                IpAddress = u.IpAddress
            })
            .ToListAsync();
    }
}

public class DashboardStats
{
    public int TotalUsers { get; set; }
    public int TotalApiKeys { get; set; }
    public int ActiveApiKeys { get; set; }
    public int TotalCodelists { get; set; }
    public int TotalAuditRecords { get; set; }

    public int RequestsToday { get; set; }
    public int RequestsWeek { get; set; }
    public int RequestsMonth { get; set; }
    public int RequestsAllTime { get; set; }

    public int JsonRequestCount { get; set; }
    public int XmlRequestCount { get; set; }

    public List<EndpointStats> TopEndpoints { get; set; } = new();
    public List<UserApiStats> TopUsers { get; set; } = new();
    public Dictionary<string, long> DailyStats { get; set; } = new();
    public List<RecentRequest> RecentRequests { get; set; } = new();
    public List<Data.Entities.AuditLog> RecentAuditLogs { get; set; } = new();
    public List<ApplicationUser> RecentUsers { get; set; } = new();
}

public class EndpointStats
{
    public string Endpoint { get; set; } = string.Empty;
    public int RequestCount { get; set; }
    public double? AvgResponseTimeMs { get; set; }
}

public class UserApiStats
{
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int RequestCount { get; set; }
}

public class RecentRequest
{
    public DateTime Timestamp { get; set; }
    public string Endpoint { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int? ResponseStatus { get; set; }
    public long? ResponseTimeMs { get; set; }
    public string? IpAddress { get; set; }
}

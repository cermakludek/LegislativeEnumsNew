using System.Text.Json;
using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;
using LegislativeEnumsNew.Filters;
using LegislativeEnumsNew.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/notifications")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly NotificationBroadcaster _broadcaster;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(
        ApplicationDbContext context,
        NotificationBroadcaster broadcaster,
        ILogger<NotificationsController> logger)
    {
        _context = context;
        _broadcaster = broadcaster;
        _logger = logger;
    }

    /// <summary>
    /// Get recent notifications
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetAll([FromQuery] int count = 20)
    {
        var notifications = await _context.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .Take(Math.Min(count, 100))
            .ToListAsync();
        return Ok(notifications);
    }

    /// <summary>
    /// Get notification by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetById(long id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return NotFound();
        return Ok(notification);
    }

    /// <summary>
    /// SSE streaming endpoint for real-time notifications
    /// </summary>
    [HttpGet("stream")]
    [Produces("text/event-stream")]
    public async Task Stream(CancellationToken cancellationToken)
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");
        Response.Headers.Append("X-Accel-Buffering", "no");

        var clientId = _broadcaster.Subscribe();
        _logger.LogInformation("SSE client connected: {ClientId}", clientId);

        try
        {
            var reader = _broadcaster.GetReader(clientId);
            if (reader == null)
            {
                _logger.LogWarning("Failed to get reader for client {ClientId}", clientId);
                return;
            }

            // Send initial connection event
            await WriteEventAsync("connected", new { clientId, timestamp = DateTime.UtcNow });

            // Send recent notifications as initial data
            var recentNotifications = await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .ToListAsync(cancellationToken);

            foreach (var notification in recentNotifications.OrderBy(n => n.CreatedAt))
            {
                await WriteEventAsync("notification", new NotificationDto
                {
                    Id = notification.Id,
                    EntityType = notification.EntityType,
                    EntityId = notification.EntityId,
                    EntityCode = notification.EntityCode,
                    ChangeType = notification.ChangeType.ToString(),
                    ChangedBy = notification.ChangedBy,
                    CreatedAt = notification.CreatedAt,
                    Message = notification.Message
                });
            }

            // Keep connection alive and stream new notifications
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (await reader.WaitToReadAsync(cancellationToken))
                    {
                        while (reader.TryRead(out var notification))
                        {
                            await WriteEventAsync("notification", notification);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SSE stream for client {ClientId}", clientId);
        }
        finally
        {
            _broadcaster.Unsubscribe(clientId);
            _logger.LogInformation("SSE client disconnected: {ClientId}", clientId);
        }
    }

    /// <summary>
    /// Get current SSE client count
    /// </summary>
    [HttpGet("stream/status")]
    public ActionResult GetStreamStatus()
    {
        return Ok(new { connectedClients = _broadcaster.ClientCount });
    }

    private async Task WriteEventAsync(string eventType, object data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await Response.WriteAsync($"event: {eventType}\n");
        await Response.WriteAsync($"data: {json}\n\n");
        await Response.Body.FlushAsync();
    }
}

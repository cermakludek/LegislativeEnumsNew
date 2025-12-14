using System.Text.Json;
using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Services;

public interface INotificationService
{
    Task<Notification> CreateNotificationAsync(string entityType, long entityId, string? entityCode, ChangeType changeType, string changedBy);
    Task<List<Notification>> GetRecentNotificationsAsync(int count = 20);
    Task<int> GetUnreadCountAsync(DateTime since);
    Task ClearOldNotificationsAsync(int keepDays = 30);
}

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly NotificationBroadcaster _broadcaster;

    public NotificationService(ApplicationDbContext dbContext, NotificationBroadcaster broadcaster)
    {
        _dbContext = dbContext;
        _broadcaster = broadcaster;
    }

    public async Task<Notification> CreateNotificationAsync(string entityType, long entityId, string? entityCode, ChangeType changeType, string changedBy)
    {
        var notification = new Notification
        {
            EntityType = entityType,
            EntityId = entityId,
            EntityCode = entityCode,
            ChangeType = changeType,
            ChangedBy = changedBy,
            CreatedAt = DateTime.UtcNow,
            Message = GenerateMessage(entityType, entityCode, changeType, changedBy)
        };

        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();

        // Broadcast to SSE clients
        await _broadcaster.BroadcastAsync(notification);

        return notification;
    }

    public async Task<List<Notification>> GetRecentNotificationsAsync(int count = 20)
    {
        return await _dbContext.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(DateTime since)
    {
        return await _dbContext.Notifications
            .CountAsync(n => n.CreatedAt > since);
    }

    public async Task ClearOldNotificationsAsync(int keepDays = 30)
    {
        var cutoff = DateTime.UtcNow.AddDays(-keepDays);
        var oldNotifications = await _dbContext.Notifications
            .Where(n => n.CreatedAt < cutoff)
            .ToListAsync();

        _dbContext.Notifications.RemoveRange(oldNotifications);
        await _dbContext.SaveChangesAsync();
    }

    private static string GenerateMessage(string entityType, string? entityCode, ChangeType changeType, string changedBy)
    {
        var action = changeType switch
        {
            ChangeType.Create => "created",
            ChangeType.Update => "updated",
            ChangeType.Delete => "deleted",
            _ => "modified"
        };

        var code = string.IsNullOrEmpty(entityCode) ? "" : $" ({entityCode})";
        return $"{entityType}{code} was {action} by {changedBy}";
    }
}

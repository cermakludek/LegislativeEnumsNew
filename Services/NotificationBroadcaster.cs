using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Channels;
using LegislativeEnumsNew.Data.Entities;

namespace LegislativeEnumsNew.Services;

public class NotificationBroadcaster
{
    private readonly ConcurrentDictionary<string, Channel<NotificationDto>> _clients = new();
    private readonly ILogger<NotificationBroadcaster> _logger;

    public NotificationBroadcaster(ILogger<NotificationBroadcaster> logger)
    {
        _logger = logger;
    }

    public string Subscribe()
    {
        var clientId = Guid.NewGuid().ToString();
        var channel = Channel.CreateUnbounded<NotificationDto>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });
        _clients.TryAdd(clientId, channel);
        _logger.LogInformation("SSE client {ClientId} subscribed. Total clients: {Count}", clientId, _clients.Count);
        return clientId;
    }

    public void Unsubscribe(string clientId)
    {
        if (_clients.TryRemove(clientId, out var channel))
        {
            channel.Writer.Complete();
            _logger.LogInformation("SSE client {ClientId} unsubscribed. Total clients: {Count}", clientId, _clients.Count);
        }
    }

    public ChannelReader<NotificationDto>? GetReader(string clientId)
    {
        return _clients.TryGetValue(clientId, out var channel) ? channel.Reader : null;
    }

    public async Task BroadcastAsync(Notification notification)
    {
        var dto = new NotificationDto
        {
            Id = notification.Id,
            EntityType = notification.EntityType,
            EntityId = notification.EntityId,
            EntityCode = notification.EntityCode,
            ChangeType = notification.ChangeType.ToString(),
            ChangedBy = notification.ChangedBy,
            CreatedAt = notification.CreatedAt,
            Message = notification.Message
        };

        var deadClients = new List<string>();

        foreach (var (clientId, channel) in _clients)
        {
            try
            {
                if (!channel.Writer.TryWrite(dto))
                {
                    deadClients.Add(clientId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to write to client {ClientId}", clientId);
                deadClients.Add(clientId);
            }
        }

        // Cleanup dead clients
        foreach (var clientId in deadClients)
        {
            Unsubscribe(clientId);
        }

        _logger.LogDebug("Broadcasted notification {NotificationId} to {Count} clients", notification.Id, _clients.Count);
    }

    public int ClientCount => _clients.Count;
}

public class NotificationDto
{
    public long Id { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public string? EntityCode { get; set; }
    public string ChangeType { get; set; } = string.Empty;
    public string ChangedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? Message { get; set; }
}

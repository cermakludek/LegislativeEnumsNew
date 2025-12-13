using System.Text.Json;
using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;

namespace LegislativeEnumsNew.Services;

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _dbContext;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AuditService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogCreateAsync<T>(T entity, string entityCode, string userName) where T : class
    {
        var auditLog = new AuditLog
        {
            EntityType = typeof(T).Name,
            EntityId = GetEntityId(entity),
            EntityCode = entityCode,
            ChangeType = ChangeType.Create,
            ChangedBy = userName,
            ChangedAt = DateTime.UtcNow,
            NewValues = SerializeEntity(entity)
        };

        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task LogUpdateAsync<T>(T oldEntity, T newEntity, string entityCode, string userName) where T : class
    {
        var auditLog = new AuditLog
        {
            EntityType = typeof(T).Name,
            EntityId = GetEntityId(newEntity),
            EntityCode = entityCode,
            ChangeType = ChangeType.Update,
            ChangedBy = userName,
            ChangedAt = DateTime.UtcNow,
            OldValues = SerializeEntity(oldEntity),
            NewValues = SerializeEntity(newEntity)
        };

        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task LogDeleteAsync<T>(T entity, string entityCode, string userName) where T : class
    {
        var auditLog = new AuditLog
        {
            EntityType = typeof(T).Name,
            EntityId = GetEntityId(entity),
            EntityCode = entityCode,
            ChangeType = ChangeType.Delete,
            ChangedBy = userName,
            ChangedAt = DateTime.UtcNow,
            OldValues = SerializeEntity(entity)
        };

        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }

    private static long GetEntityId<T>(T entity) where T : class
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null)
        {
            var value = idProperty.GetValue(entity);
            if (value is long longValue) return longValue;
            if (value is int intValue) return intValue;
        }
        return 0;
    }

    private static string SerializeEntity<T>(T entity) where T : class
    {
        try
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
                .ToDictionary(p => p.Name, p => p.GetValue(entity));

            return JsonSerializer.Serialize(properties, _jsonOptions);
        }
        catch
        {
            return "{}";
        }
    }

    private static bool IsSimpleType(Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return underlyingType.IsPrimitive
            || underlyingType.IsEnum
            || underlyingType == typeof(string)
            || underlyingType == typeof(decimal)
            || underlyingType == typeof(DateTime)
            || underlyingType == typeof(DateOnly)
            || underlyingType == typeof(TimeOnly)
            || underlyingType == typeof(DateTimeOffset)
            || underlyingType == typeof(Guid);
    }
}

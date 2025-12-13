using LegislativeEnumsNew.Data.Entities;

namespace LegislativeEnumsNew.Services;

public interface IAuditService
{
    Task LogCreateAsync<T>(T entity, string entityCode, string userName) where T : class;
    Task LogUpdateAsync<T>(T oldEntity, T newEntity, string entityCode, string userName) where T : class;
    Task LogDeleteAsync<T>(T entity, string entityCode, string userName) where T : class;
}

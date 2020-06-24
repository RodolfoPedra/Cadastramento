using System.Linq;
using Cadastramento.ModelData.Logic.Cadastramento;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces
{
    public interface ITrackerContext : IDbContext
    {
        IQueryable<auditlog> GetLogs(string entityFullName);
        IQueryable<auditlog> GetLogs(string entityFullName, object primaryKey);
        IQueryable<auditlog> GetLogs<TEntity>();
        IQueryable<auditlog> GetLogs<TEntity>(object primaryKey);
        int SaveChanges(object userName);
    }
}
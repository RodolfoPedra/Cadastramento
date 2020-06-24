using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Models;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext
{
    public static class CommonTracker
    {
        public static void AuditChanges(ITrackerContext dbContext, object userName, List<string> listaExcecoes)
        {
            // Get all Deleted/Modified entities (not Unmodified or Detached or Added)
            foreach (
                DbEntityEntry ent in
                    dbContext.ChangeTracker.Entries()
                        .Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                using (var auditer = new LogAuditor(ent))
                {
                    auditlog record = auditer.CreateLogRecord(userName,
                        ent.State == EntityState.Modified ? EventType.Modified : EventType.Deleted, dbContext, listaExcecoes);
                    if (record != null)
                    {
                        dbContext.Set<auditlog>().Add(record);
                    }
                }
            }
        }

        public static IEnumerable<DbEntityEntry> GetAdditions(ITrackerContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
        }

        public static void AuditAdditions(ITrackerContext dbContext, object userName,
            IEnumerable<DbEntityEntry> addedEntries, List<string> listaExcecoes)
        {
            // Get all Added entities
            foreach (DbEntityEntry ent in addedEntries)
            {
                using (var auditer = new LogAuditor(ent))
                {
                    auditlog record = auditer.CreateLogRecord(userName, EventType.Added, dbContext, listaExcecoes);
                    if (record != null)
                    {
                        dbContext.Set<auditlog>().Add(record);
                    }
                }
            }
        }

        private static IEnumerable<string> EntityTypeNames<TEntity>()
        {
            Type entityType = typeof(TEntity);
            return typeof(TEntity).Assembly.GetTypes().Where(t => t.IsSubclassOf(entityType) || t.FullName == entityType.FullName).Select(m => m.FullName);
        }

        /// <summary>
        ///     Get all logs for the given model type
        /// </summary>
        /// <typeparam name="TEntity">Type of domain model</typeparam>
        /// <returns></returns>
        public static IQueryable<auditlog> GetLogs<TEntity>(ITrackerContext context)
        {
            IEnumerable<string> entityTypeNames = EntityTypeNames<TEntity>();
            string entityTypeName = typeof(TEntity).Name;
            return context.Set<auditlog>().Where(x => entityTypeNames.Contains(x.TypeFullName));
        }

        /// <summary>
        ///     Get all logs for the enitity type name
        /// </summary>
        /// <param name="entityTypeName">Name of entity type</param>
        /// <returns></returns>
        public static IQueryable<auditlog> GetLogs(ITrackerContext context, string entityTypeName)
        {
            return context.Set<auditlog>().Where(x => x.TypeFullName == entityTypeName);
        }

        /// <summary>
        ///     Get all logs for the given model type for a specific record
        /// </summary>
        /// <typeparam name="TEntity">Type of domain model</typeparam>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns></returns>
        public static IQueryable<auditlog> GetLogs<TEntity>(ITrackerContext context, object primaryKey)
        {
            string key = primaryKey.ToString();
            string entityTypeName = typeof(TEntity).Name;
            IEnumerable<string> entityTypeNames = EntityTypeNames<TEntity>();

            return context.Set<auditlog>().Where(x => entityTypeNames.Contains(x.TypeFullName) && x.RecordId == key);
        }

        /// <summary>
        ///     Get all logs for the given entity name for a specific record
        /// </summary>
        /// <param name="entityTypeName">entity type name</param>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns></returns>
        public static IQueryable<auditlog> GetLogs(ITrackerContext context, string entityTypeName, object primaryKey)
        {
            string key = primaryKey.ToString();
            return context.Set<auditlog>().Where(x => x.TypeFullName == entityTypeName && x.RecordId == key);
        }

        /// <summary>
        ///     Get the id of the most recently created log for the given table name for a specific record
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns>Log id</returns>
        public static long GetLastAuditLogId(ITrackerContext context, string tableName, object primaryKey)
        {
            string key = primaryKey.ToString();
            return context.Set<auditlog>().Where(x => x.TypeFullName == tableName && x.RecordId == key).OrderByDescending(x => x.AuditLogId).Select(x => x.AuditLogId).FirstOrDefault();
        }
    }
}

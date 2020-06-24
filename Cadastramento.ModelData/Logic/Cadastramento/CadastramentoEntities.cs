using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Configuration;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces;

namespace Cadastramento.ModelData.Logic.Cadastramento
{
    public partial class CadastramentoEntities: ITrackerContext
    {
        public static List<string> listaExcecoes = new List<string>();

        /// <summary>
        ///     This method saves the model changes to the database.
        ///     If the tracker for an entity is active, it will also put the old values in tracking table.
        ///     Always use this method instead of SaveChanges() whenever possible.
        /// </summary>
        /// <param name="userName">Username of the logged in identity</param>
        /// <returns>Returns the number of objects written to the underlying database.</returns>
        public virtual int SaveChanges(object userName)
        {
            if (!listaExcecoes.Any())
                listaExcecoes = this.auditlogexcecao.Select(ent => ent.EntityTypeName).ToList();

            if (!GlobalTrackingConfig.Enabled) return base.SaveChanges();

            CommonTracker.AuditChanges(this, userName, listaExcecoes);

            IEnumerable<DbEntityEntry> addedEntries = CommonTracker.GetAdditions(this);
            // Call the original SaveChanges(), which will save both the changes made and the audit records...Note that added entry auditing is still remaining.
            int result = base.SaveChanges();
            //By now., we have got the primary keys of added entries of added entiries because of the call to savechanges.

            CommonTracker.AuditAdditions(this, userName, addedEntries, listaExcecoes);

            //save changes to audit of added entries
            base.SaveChanges();
            return result;
        }

        ///// <summary>
        /////     This method saves the model changes to the database.
        /////     If the tracker for an entity is active, it will also put the old values in tracking table.
        ///// </summary>
        ///// <returns>Returns the number of objects written to the underlying database.</returns>
        //public override int SaveChanges()
        //{
        //    if (!GlobalTrackingConfig.Enabled) return base.SaveChanges();
        //    //var user = Thread.CurrentPrincipal?.Identity?.Name ?? "Anonymous"; 
        //    return SaveChanges(null);
        //}

        /// <summary>
        ///     Get all logs for the given model type
        /// </summary>
        /// <typeparam name="TTable">Type of domain model</typeparam>
        /// <returns></returns>
        public IQueryable<auditlog> GetLogs<TTable>()
        {
            return CommonTracker.GetLogs<TTable>(this);
        }

        /// <summary>
        ///     Get all logs for the given entity name
        /// </summary>
        /// <param name="entityName">full name of entity</param>
        /// <returns></returns>
        public IQueryable<auditlog> GetLogs(string entityName)
        {
            return CommonTracker.GetLogs(this, entityName);
        }

        /// <summary>
        ///     Get all logs for the given model type for a specific record
        /// </summary>
        /// <typeparam name="TTable">Type of domain model</typeparam>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns></returns>
        public IQueryable<auditlog> GetLogs<TTable>(object primaryKey)
        {
            return CommonTracker.GetLogs<TTable>(this, primaryKey);
        }

        /// <summary>
        ///     Get all logs for the given entity name for a specific record
        /// </summary>
        /// <param name="entityName">full name of entity</param>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns></returns>
        public IQueryable<auditlog> GetLogs(string entityName, object primaryKey)
        {
            return CommonTracker.GetLogs(this, entityName, primaryKey);
        }

        /// <summary>
        ///     Get the id of the most recently created log for the given table name for a specific record
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <param name="primaryKey">primary key of record</param>
        /// <returns>Log id</returns>
        public long GetLastAuditLogId(string tableName, object primaryKey)
        {
            return CommonTracker.GetLastAuditLogId(this, tableName, primaryKey);
        }
    }
}

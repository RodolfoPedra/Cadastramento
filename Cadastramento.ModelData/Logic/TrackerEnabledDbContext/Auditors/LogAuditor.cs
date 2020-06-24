using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Configuration;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Extensions;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Models;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors
{
    internal class LogAuditor : IDisposable
    {
        private readonly DbEntityEntry _dbEntry;

        internal LogAuditor(DbEntityEntry dbEntry)
        {
            _dbEntry = dbEntry;
        }

        public void Dispose()
        {
        }

        internal auditlog CreateLogRecord(object userName, EventType eventType, ITrackerContext context, List<string> listaExcecoes)
        {
            Type entityType = _dbEntry.Entity.GetType().GetEntityType();

            if (listaExcecoes.Contains(entityType.Name))
            {
                return null;
            }

            DateTime changeTime = DateTime.Now;

            //todo: make this a static class
            var mapping = new DbMapping(context, entityType);

            List<PropertyConfiguerationKey> keyNames = mapping.PrimaryKeys().ToList();

            var newlog = new auditlog
            {
                UserName = userName?.ToString(),
                EventDateUTC = changeTime,
                EventType = (int)eventType,
                TypeFullName = entityType.FullName,
                RecordId = GetPrimaryKeyValuesOf(_dbEntry, keyNames).ToString()
            };

            var detailsAuditor = GetDetailsAuditor(eventType, newlog);

            detailsAuditor.CreateLogDetails().ToList().ForEach(newlog.auditlogdetail.Add);

            if (newlog.auditlogdetail.Any())
                return newlog;

            return null;
        }

        private ChangeLogDetailsAuditor GetDetailsAuditor(EventType eventType, auditlog newlog)
        {
            switch (eventType)
            {
                case EventType.Added:
                    return new AdditionLogDetailsAuditor(_dbEntry, newlog);

                case EventType.Deleted:
                    return new DeletetionLogDetailsAuditor(_dbEntry, newlog);

                case EventType.Modified:
                    return new ChangeLogDetailsAuditor(_dbEntry, newlog);

                default:
                    return null;
            }
        }

        private static object GetPrimaryKeyValuesOf(
            DbEntityEntry dbEntry,
            List<PropertyConfiguerationKey> properties)
        {
            if (properties.Count == 1)
            {
                return dbEntry.GetDatabaseValues().GetValue<object>(properties.Select(x => x.PropertyName).First());
            }
            if (properties.Count > 1)
            {
                string output = "[";

                output += string.Join(",",
                    properties.Select(colName => dbEntry.GetDatabaseValues().GetValue<object>(colName.PropertyName)));

                output += "]";
                return output;
            }
            throw new KeyNotFoundException("key not found for " + dbEntry.Entity.GetType().FullName);
        }
    }
}
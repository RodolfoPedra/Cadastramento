using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors.Comparators;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Configuration;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Extensions;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors
{
    public class ChangeLogDetailsAuditor : ILogDetailsAuditor
    {
        protected readonly DbEntityEntry DbEntry;
        private readonly auditlog _log;

        public ChangeLogDetailsAuditor(DbEntityEntry dbEntry, auditlog log)
        {
            DbEntry = dbEntry;
            _log = log;
        }

        public IEnumerable<auditlogdetail> CreateLogDetails()
        {
            Type entityType = DbEntry.Entity.GetType().GetEntityType();

            foreach (string propertyName in PropertyNamesOfEntity())
            {
                if (PropertyTrackingConfiguration.IsTrackingEnabled(
                    new PropertyConfiguerationKey(propertyName, entityType.FullName), entityType ) 
                    && IsValueChanged(propertyName))
                {
                    var entityProperties = entityType.GetProperties();
                    foreach (var entityProperty in entityProperties)
                    {
                        if (Type.GetTypeCode(entityProperty.PropertyType) == TypeCode.String)
                        {
                            entityProperty.SetValue(DbEntry.Entity, Convert.ToString(entityProperty.GetValue(DbEntry.Entity, null)).ToUpper(), null);
                        }
                    }

                    yield return new auditlogdetail
                    {
                        PropertyName = propertyName,
                        OriginalValue = OriginalValue(propertyName)?.ToString(),
                        NewValue = CurrentValue(propertyName)?.ToString(),
                        auditlog = _log
                    };
                }
            }
        }

        protected internal virtual EntityState StateOfEntity()
        {
            return DbEntry.State;
        }

        private IEnumerable<string> PropertyNamesOfEntity()
        {
            var propertyValues = (StateOfEntity() == EntityState.Added)
                ? DbEntry.CurrentValues
                : DbEntry.OriginalValues;
            return propertyValues.PropertyNames;
        }

        protected virtual bool IsValueChanged(string propertyName)
        {
            var prop = DbEntry.Property(propertyName);
            var propertyType = DbEntry.Entity.GetType().GetProperty(propertyName).PropertyType;

            object originalValue = OriginalValue(propertyName);

            Comparator comparator = ComparatorFactory.GetComparator(propertyType);

            var changed = (StateOfEntity() == EntityState.Modified
                && prop.IsModified && !comparator.AreEqual(CurrentValue(propertyName), originalValue));
            return changed;
        }

        protected virtual object OriginalValue(string propertyName)
        {
            object originalValue = null;

            originalValue = DbEntry.GetDatabaseValues().GetValue<object>(propertyName);

            //if (GlobalTrackingConfig.DisconnectedContext)
            //{
            //    originalValue = DbEntry.GetDatabaseValues().GetValue<object>(propertyName);
            //}
            //else
            //{
            //    originalValue = DbEntry.Property(propertyName).OriginalValue;
            //}

            return originalValue;
        }

        protected virtual object CurrentValue(string propertyName)
        {
            var value = DbEntry.Property(propertyName).CurrentValue;
            return value;
        }
    }
}
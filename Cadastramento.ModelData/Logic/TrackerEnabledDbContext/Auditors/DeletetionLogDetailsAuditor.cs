using System.Data.Entity.Infrastructure;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors.Comparators;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Configuration;
using Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Extensions;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Auditors
{
    public class DeletetionLogDetailsAuditor: ChangeLogDetailsAuditor
    {
        public DeletetionLogDetailsAuditor(DbEntityEntry dbEntry, auditlog log) : base(dbEntry, log)
        {
        }

        protected override bool IsValueChanged(string propertyName)
        {
            if (GlobalTrackingConfig.TrackEmptyPropertiesOnAdditionAndDeletion)
                return true;

            var propertyType = DbEntry.Entity.GetType().GetProperty(propertyName).PropertyType;
            object defaultValue = propertyType.DefaultValue();
            object orginalvalue = OriginalValue(propertyName);

            Comparator comparator = ComparatorFactory.GetComparator(propertyType);

            return !comparator.AreEqual(defaultValue, orginalvalue);
        }

        protected override object CurrentValue(string propertyName)
        {
            return null;
        }
    }
}

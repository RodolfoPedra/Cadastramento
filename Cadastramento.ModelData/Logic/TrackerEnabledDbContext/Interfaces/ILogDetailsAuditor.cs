using System.Collections.Generic;
using Cadastramento.ModelData.Logic.Cadastramento;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Interfaces
{
    public interface ILogDetailsAuditor
    {
        IEnumerable<auditlogdetail> CreateLogDetails();
    }
}
namespace Cadastramento.Util.DataTables
{
    using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
    using System.Linq;

    public static class DataTablesExtensionsV2
    {


        public static DataTablesReturnBuilder<T> ToDataTables<T>(this IQueryable<T> valor, IDataTablesRequest request)
        {
            return new DataTablesReturnBuilder<T>(valor, request);
        }
    }


}

namespace Cadastramento.Util.DataTables
{
    using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
    using Cadastramento.Util.DataTables.DataTables.AspNet.Mvc5;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using static DataTablesUtil;

    public static class DataTablesUtil
    {


        public enum TipoFiltro
        {
            Contem,
            Igual,
            Customizado
        }
    }

    public class DataTablesReturnBuilder<T>
    {
        private int _qtdeTotal;
        private IQueryable<T> _listaFiltrada;
        private readonly IDataTablesRequest _request;


        public DataTablesReturnBuilder(IQueryable<T> lista, IDataTablesRequest request)
        {

            _qtdeTotal = lista.Count();

            _listaFiltrada = lista;


            _request = request;
        }

        public DataTablesReturnBuilder<T> AddFiltro(Expression<Func<T, bool>> filtro)
        {
            _listaFiltrada = _listaFiltrada.Where(filtro);
            return this;
        }

        public DataTablesReturnBuilder<T> AddFiltro<TParam>(string nomeFiltro, TipoFiltro tipoFiltro)
        {
            if (_request.AdditionalParameters.ContainsKey(nomeFiltro))
                AddFiltro(nomeFiltro, tipoFiltro, _request.Get<TParam>(nomeFiltro));

            return this;
        }

        public DataTablesReturnBuilder<T> AddFiltro<TParam>(string nomeFiltro, TipoFiltro tipoFiltro, Func<TParam, bool> condicaoParaAplicarOFiltro)
        {
            if (_request.AdditionalParameters.ContainsKey(nomeFiltro))
                if (condicaoParaAplicarOFiltro(_request.Get<TParam>(nomeFiltro)))
                    return AddFiltro(nomeFiltro, tipoFiltro, _request.Get<TParam>(nomeFiltro));

            return this;
        }

        public DataTablesReturnBuilder<T> AddFiltro(string nomeFiltro, string operador)
        {
            var valor = _request.Get<string>(nomeFiltro);
            _listaFiltrada = _listaFiltrada.Where(string.Concat(nomeFiltro, " ", operador, " ", ProcessaValor(valor)));
            return this;
        }

        private DataTablesReturnBuilder<T> AddFiltro<TParam>(string nomeFiltro, TipoFiltro tipoFiltro, TParam valor)
        {
            switch (tipoFiltro)
            {
                case TipoFiltro.Contem:
                    _listaFiltrada = _listaFiltrada.Where(string.Concat(nomeFiltro, ".contains(\"", valor, "\")"));
                    break;
                case TipoFiltro.Igual:
                    if (typeof(TParam) == typeof(DateTime) || typeof(TParam) == typeof(DateTime?))
                        _listaFiltrada = _listaFiltrada.Where($"{nomeFiltro} = DateTime({valor:yyyy, MM, dd, HH, mm, ss})");
                    else
                        _listaFiltrada = _listaFiltrada.Where($"{nomeFiltro} = {ProcessaValor(valor.ToString())}");
                    break;
                default:
                    break;
            }

            return this;
        }


        public DataTablesJsonResult ToReturn<TResult>(Func<T, TResult> select)
        {
            var qtdeFiltrado = _listaFiltrada.Count();

            var sortColumm = _request.Columns.FirstOrDefault(x => x.Sort != null);

            if (sortColumm.Sort.Direction == SortDirection.Descending)
                _listaFiltrada = _listaFiltrada.OrderBy(sortColumm.Name + " desc").Skip(_request.Start).Take(_request.Length);
            else
                _listaFiltrada = _listaFiltrada.OrderBy(sortColumm.Name).Skip(_request.Start).Take(_request.Length);


            var listaFinal = _listaFiltrada.ToList().Select(select);

            var response = DataTablesResponse.Create(_request, _qtdeTotal, qtdeFiltrado, listaFinal);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }


        private string ProcessaValor(string valor)
        {
            int n;
            if (valor.Contains("DateTime") || int.TryParse(valor, out n))
                return valor;
            else
                return "\"" + valor + "\"";
        }
    }
}

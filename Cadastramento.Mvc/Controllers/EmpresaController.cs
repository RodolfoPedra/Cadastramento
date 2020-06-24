using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Mvc.Models;
using Cadastramento.Service;
using Cadastramento.Util;
using Cadastramento.Util.DataTables;
using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
using Cadastramento.Util.DataTables.DataTables.AspNet.Mvc5;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    public class EmpresaController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.MunicipioLista = GerarListaTodosMunicipios();

            return View();
        }

        public JsonResult IndexGrid(IDataTablesRequest request)
        {
            var srv = new BaseService<empresa>();
            var data = srv.ObterTodos();

            var sortColumn = request.Columns.FirstOrDefault(x => x.Sort != null);

            #region Filtro de Busca

            var filteredData = data;

            if (request.AdditionalParameters.Any())
            {
                foreach (var filter in request.AdditionalParameters)
                {
                    switch (filter.Key)
                    {
                        case "cnpj":
                            var cnpj = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(cnpj))
                            {
                                cnpj = cnpj.apenasNumeros();
                                filteredData = filteredData.Where(x => x.cnpj == cnpj);
                            }
                            break;
                        case "razaosocial":
                            var razaosocial = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(razaosocial))
                                filteredData = filteredData.Where(x => x.razaosocial.Contains(razaosocial));
                            break;
                        case "municipioid":
                            var municipioid = request.Get<int?>(filter.Key);
                            if (municipioid != null)
                                filteredData = filteredData.Where(x => x.municipioid == municipioid);
                            break;
                    }
                }
            }

            #endregion

            IQueryable<empresa> dataPage;
            if (sortColumn != null)
            {
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                    dataPage = filteredData.OrderBy(sortColumn.Name + " desc").Skip(request.Start).Take(request.Length);
                else
                    dataPage = filteredData.OrderBy(sortColumn.Name).Skip(request.Start).Take(request.Length);
            }
            else
                dataPage = filteredData.OrderBy(x => x.razaosocial).Skip(request.Start).Take(request.Length);

            var lista = dataPage.ToList().Select(u => new
            {
                u.empresaid,
                cnpj = u.cnpj.FormataCnpj(),
                u.razaosocial,
                municipio = u.municipio != null ? (u.municipio.nome + " - " + u.municipio.uf) : ""
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), lista);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        private void PreparaViewBags()
        {
            ViewBag.UfLista = GerarListaUfs();
        }

        public ActionResult Incluir()
        {
            PreparaViewBags();

            return View();
        }
    }
}
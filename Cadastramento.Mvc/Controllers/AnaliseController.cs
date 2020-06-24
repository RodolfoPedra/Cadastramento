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
    public class AnaliseController : BaseController
    {
        // GET: Analise
        public ActionResult Index()
        {
            PreparaViewBag();

            return View();
        }

        public JsonResult IndexGridMotorista(IDataTablesRequest request)
        {
            var srv = new BaseService<motorista>();
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
                        case "datahorainclusao":
                            var datahorainclusao = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(datahorainclusao))
                                filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                            break;
                        case "situacao":
                            var situacaocadastroid = request.Get<int>(filter.Key);
                            if (situacaocadastroid != 0)
                                filteredData = filteredData.Where(x => x.situacaocadastroid == situacaocadastroid);
                            break;
                    }
                }
            }

            #endregion

            IQueryable<motorista> dataPage;
            if (sortColumn != null)
            {
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                    dataPage = filteredData.OrderBy(sortColumn.Name + " desc").Skip(request.Start).Take(request.Length);
                else
                    dataPage = filteredData.OrderBy(sortColumn.Name).Skip(request.Start).Take(request.Length);
            }
            else
                dataPage = filteredData.OrderBy(x => x.datahorainclusao).Skip(request.Start).Take(request.Length);

            var lista = dataPage.ToList().Select(u => new
            {
                u.motoristaid,
                u.protocolo,
                u.nome,
                u.cpf,
                u.cnh,
                u.datahorainclusao,
                u.situacaocadastro.descricao                
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), lista);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexGridVeiculo(IDataTablesRequest request)
        {
            var srv = new BaseService<veiculo>();
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
                        case "datahorainclusao":
                            var datahorainclusao = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(datahorainclusao))
                                filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                            break;
                        case "situacao":
                            var situacaocadastroid = request.Get<int>(filter.Key);
                            if (situacaocadastroid != 0)
                                filteredData = filteredData.Where(x => x.situacaocadastroid == situacaocadastroid);
                            break;
                    }
                }
            }

            #endregion

            IQueryable<veiculo> dataPage;
            if (sortColumn != null)
            {
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                    dataPage = filteredData.OrderBy(sortColumn.Name + " desc").Skip(request.Start).Take(request.Length);
                else
                    dataPage = filteredData.OrderBy(sortColumn.Name).Skip(request.Start).Take(request.Length);
            }
            else
                dataPage = filteredData.OrderBy(x => x.datahorainclusao).Skip(request.Start).Take(request.Length);

            var lista = dataPage.ToList().Select(u => new
            {
                u.veiculoid,
                u.protocolo,
                u.placa,
                u.renavam,                
                u.datahorainclusao,
                u.situacaocadastro.descricao
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), lista);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexGridCarroceria(IDataTablesRequest request)
        {
            var srv = new BaseService<carroceria>();
            var data = srv.ObterTodos().Where(e=> e.situacaocadastroid == 1);

            var sortColumn = request.Columns.FirstOrDefault(x => x.Sort != null);

            #region Filtro de Busca

            var filteredData = data;

            if (request.AdditionalParameters.Any())
            {
                foreach (var filter in request.AdditionalParameters)
                {
                    switch (filter.Key)
                    {                        
                        case "datahorainclusao":
                            var datahorainclusao = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(datahorainclusao))
                                filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                            break;
                        case "situacao":
                            var situacaocadastroid = request.Get<int>(filter.Key);
                            if (situacaocadastroid != 0)
                                filteredData = filteredData.Where(x => x.situacaocadastroid == situacaocadastroid);
                            break;
                    }
                }
            }

            #endregion

            IQueryable<carroceria> dataPage;
            if (sortColumn != null)
            {
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                    dataPage = filteredData.OrderBy(sortColumn.Name + " desc").Skip(request.Start).Take(request.Length);
                else
                    dataPage = filteredData.OrderBy(sortColumn.Name).Skip(request.Start).Take(request.Length);
            }
            else
                dataPage = filteredData.OrderBy(x => x.datahorainclusao).Skip(request.Start).Take(request.Length);

            var lista = dataPage.ToList().Select(u => new
            {
                u.carroceriaid,
                u.protocolo,
                u.placa,
                u.renavam,
                u.datahorainclusao,
                u.situacaocadastro.descricao
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), lista);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        private void PreparaViewBag()
        {
            ViewBag.SituacaoLista = ListaSituacao();
        }

        public ActionResult AnalisarMotorista(int id)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }

        public ActionResult AnalisarVeiculo(int id)
        {
            var srv = new BaseService<veiculo>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }

        public ActionResult AnalisarCarroceria(int id)
        {
            var srv = new BaseService<carroceria>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }


    }
}
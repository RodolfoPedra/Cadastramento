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
    [Authorize]
    public class CadeiaProdutivaController : BaseController
    {
        public ActionResult Index()
        {
            PreparaViewBag();

            return View();
        }

        public JsonResult IndexGrid(IDataTablesRequest request)
        {
            var srv = new BaseService<cadeiaprodutiva>();
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
                        case "descricao":
                            var descricao = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(descricao))
                                filteredData = filteredData.Where(x => x.descricao.Contains(descricao));
                            break;
                        case "situacao":
                            var situacao = request.Get<string>(filter.Key);
                            if (!string.IsNullOrEmpty(situacao))
                                filteredData = filteredData.Where(x => x.situacao == situacao);
                            break;
                    }
                }
            }

            #endregion

            IQueryable<cadeiaprodutiva> dataPage;
            if (sortColumn != null)
            {
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                    dataPage = filteredData.OrderBy(sortColumn.Name + " desc").Skip(request.Start).Take(request.Length);
                else
                    dataPage = filteredData.OrderBy(sortColumn.Name).Skip(request.Start).Take(request.Length);
            }
            else
                dataPage = filteredData.OrderBy(x => x.descricao).Skip(request.Start).Take(request.Length);

            var lista = dataPage.ToList().Select(u => new
            {
                u.cadeiaprodutivaid,
                u.descricao,
                situacao = u.situacao == "A" ? "ATIVO" : "INATIVO"
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), lista);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        private void PreparaViewBag()
        {
            ViewBag.SituacaoLista = GerarListaSituacao();
        }

        public ActionResult Incluir()
        {
            PreparaViewBag();

            return View();
        }

        [HttpPost]
        public ActionResult Incluir(cadeiaprodutiva model)
        {
            try
            {
                var srv = new BaseService<cadeiaprodutiva>();

                model.usuarioidinclusao = SessaoUsuario.Sessao.usuarioid;
                model.datahorainclusao = DateTime.Now;

                srv.Incluir(model);
                srv.Salvar(SessaoUsuario.Sessao.login);

                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                PreparaViewBag();
                return View(model);
            }
        }

        public ActionResult Alterar(int id)
        {
            var srv = new BaseService<cadeiaprodutiva>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }

        [HttpPost]
        public ActionResult Alterar(cadeiaprodutiva model)
        {
            try
            {
                var srv = new BaseService<cadeiaprodutiva>();

                model.usuarioidalteracao = SessaoUsuario.Sessao.usuarioid;
                model.datahoraalteracao = DateTime.Now;

                srv.Alterar(model);
                srv.Salvar(SessaoUsuario.Sessao.login);

                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                PreparaViewBag();
                return View(model);
            }
        }

        public ActionResult Visualizar(int id)
        {
            var srv = new BaseService<cadeiaprodutiva>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }

        public ActionResult Excluir(int id)
        {
            var srv = new BaseService<cadeiaprodutiva>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }

        [HttpPost]
        public ActionResult Excluir(cadeiaprodutiva model)
        {
            try
            {
                var srv = new BaseService<cadeiaprodutiva>();

                srv.Excluir(ent => ent.cadeiaprodutivaid == model.cadeiaprodutivaid);
                srv.Salvar(SessaoUsuario.Sessao.login);

                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                PreparaViewBag();
                return View(model);
            }
        }
    }
}
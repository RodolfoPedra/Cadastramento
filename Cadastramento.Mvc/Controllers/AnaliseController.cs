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
                        //case "datahorainclusao":
                        //    var datahorainclusao = request.Get<string>(filter.Key);
                        //    if (!string.IsNullOrEmpty(datahorainclusao))
                        //        filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                        //    break;
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
                        //case "datahorainclusao":
                        //    var datahorainclusao = request.Get<string>(filter.Key);
                        //    if (!string.IsNullOrEmpty(datahorainclusao))
                        //        filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                        //    break;
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
                        //case "datahorainclusao":
                        //    var datahorainclusao = request.Get<string>(filter.Key);
                        //    if (!string.IsNullOrEmpty(datahorainclusao))
                        //        filteredData = filteredData.Where(x => x.datahorainclusao == Convert.ToDateTime(datahorainclusao));
                        //    break;
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

        public ActionResult ModalAnalisar()
        {
            PreparaViewBag();
            return View();
        }


        public ActionResult FinalizarAnalise(int situacaoid, string observacao, int usuarioidinclusao, int motoristaid = 0, int veiculoid = 0, int carroceriaid = 0)
        {

            if (motoristaid > 0)
            {
                var svrMotorista = new BaseService<motorista>();
                var objmotorista = svrMotorista.Obter(motoristaid);
                objmotorista.situacaocadastroid = situacaoid;
                svrMotorista.Alterar(objmotorista);
                svrMotorista.Salvar(SessaoUsuario.Sessao.login);
            }
            if (veiculoid > 0)
            {
                var svrVeiculo = new BaseService<veiculo>();
                var objveiculo = svrVeiculo.Obter(veiculoid);
                objveiculo.situacaocadastroid = situacaoid;
                svrVeiculo.Alterar(objveiculo);
                svrVeiculo.Salvar(SessaoUsuario.Sessao.login);
            }
            if (carroceriaid > 0)
            {
                var svrCarroceria = new BaseService<carroceria>();
                var objcarroceria = svrCarroceria.Obter(carroceriaid);
                objcarroceria.situacaocadastroid = situacaoid;
                svrCarroceria.Alterar(objcarroceria);
                svrCarroceria.Salvar(SessaoUsuario.Sessao.login);
            }

            var srv = new BaseService<analise>();
            var obj = srv.Obter(situacaoid);

            try
            {
              //  obj.situacaocadastroid = situacao;
                srv.Alterar(obj);
                srv.Salvar(SessaoUsuario.Sessao.login);
                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                return View();
            }
        }


        public ActionResult FinalizarAnaliseMotorista(int id, int situacao)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);

            try
            {
                obj.situacaocadastroid = situacao;
                srv.Alterar(obj);
                srv.Salvar(SessaoUsuario.Sessao.login);
                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                return View();
            }
        }
        public ActionResult AnalisarMotorista(int id)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }
        public ActionResult FinalizarAnaliseVeiculo(int id, int situacao)
        {
            var srv = new BaseService<veiculo>();
            var obj = srv.Obter(id);

            try
            {
                obj.situacaocadastroid = situacao;
                srv.Alterar(obj);
                srv.Salvar(SessaoUsuario.Sessao.login);
                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                return View();
            }
        }
        public ActionResult AnalisarVeiculo(int id)
        {
            var srv = new BaseService<veiculo>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }
        public ActionResult FinalizarAnaliseCarroceria(int id, int situacao)
        {
            var srv = new BaseService<carroceria>();
            var obj = srv.Obter(id);

            try
            {
                obj.situacaocadastroid = situacao;
                srv.Alterar(obj);
                srv.Salvar(SessaoUsuario.Sessao.login);
                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);

                return View();
            }
        }
        public ActionResult AnalisarCarroceria(int id)
        {
            var srv = new BaseService<carroceria>();
            var obj = srv.Obter(id);

            PreparaViewBag();

            return View(obj);
        }
        public FileResult DownloadArquivoMotorista(int id)
        {
            var arq = new BaseService<motorista>().Obter(id);
            return File(arq.arquivo, arq.contenttype.ToLower(), arq.nomearquivo.ToLower());
        }
        public FileResult DownloadArquivoVeiculo(int id)
        {
            var arq = new BaseService<motorista>().Obter(id);
            return File(arq.arquivo, arq.contenttype.ToLower(), arq.nomearquivo.ToLower());
        }
        public FileResult DownloadArquivoCarroceria(int id)
        {
            var arq = new BaseService<motorista>().Obter(id);
            return File(arq.arquivo, arq.contenttype.ToLower(), arq.nomearquivo.ToLower());
        }


    }
}
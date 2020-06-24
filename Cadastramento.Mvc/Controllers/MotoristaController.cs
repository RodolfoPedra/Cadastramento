using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Mvc.Models;
using Cadastramento.Service;
using Cadastramento.Service.Logic.Cadastramento;
using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
using Cadastramento.Util.DataTables.DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    public class MotoristaController : Controller
    {
        // GET: Motorista
        public ActionResult Index()
        {
            return View();
        }     

        public ActionResult Incluir()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(motorista model)
        {
            try
            {
                var srv = new BaseService<motorista>();

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

                
                return View(model);
            }
        }

        public ActionResult Alterar(int id)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);
            
            return View(obj);
        }

        [HttpPost]
        public ActionResult Alterar(motorista model)
        {
            try
            {
                var srv = new BaseService<motorista>();

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

                
                return View(model);
            }
        }

        public ActionResult Visualizar(int id)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);


            return View(obj);
        }
            
        public ActionResult Excluir(int id)
        {
            var srv = new BaseService<motorista>();
            var obj = srv.Obter(id);
            
            return View(obj);
        }

        [HttpPost]
        public ActionResult Excluir(motorista model)
        {
            try
            {
                var srv = new BaseService<motorista>();

                srv.Excluir(ent => ent.motoristaid == model.motoristaid);
                srv.Salvar(SessaoUsuario.Sessao.login);

                EnviarMensagem("Operação realizada com sucesso.", TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);
                                
                return View(model);
            }
        }

        private void EnviarMensagem(string v, TipoMensagem vermelho)
        {
            throw new NotImplementedException();
        }

        public ActionResult Obter(string cpf, string cnh)
        {

            var srv = new detranService();
            var obj = srv.GetDadosCNH(cpf, cnh);

            return View(obj);

        }


    }
}
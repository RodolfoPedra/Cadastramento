using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Mvc.Models;
using Cadastramento.Service;
using Cadastramento.Service.Logic.Cadastramento;
using Cadastramento.Util;
using Cadastramento.Util.DataTables;
using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
using Cadastramento.Util.DataTables.DataTables.AspNet.Mvc5;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Cadastramento.Mvc.Controllers
{
    public class MotoristaController : BaseController
    {
        // GET: Motorista
        public ActionResult Index()
        {
            return View();
        }     

        public ActionResult Incluir()
        {
            PreparaViewBag();

            return View();
        }

        [HttpPost]
        public ActionResult Incluir(motorista model, HttpPostedFileBase file)
        {
            try
            {
                var srv = new BaseService<motorista>();

                var b = new BinaryReader(file.InputStream);
                model.arquivo = b.ReadBytes(file.ContentLength);
                model.contenttype = file.ContentType;
                model.nomearquivo = file.FileName;

                model.telefone = Extensao.RemoveMascara(model.telefone);
                model.usuarioidinclusao = SessaoUsuario.Sessao.usuarioid;
                model.datahorainclusao = DateTime.Now;
                model.datavalidadecadastro = DateTime.Now.AddYears(1); 
                if(model.situacaocadastroid != 2)
                {
                    model.situacaocadastroid = 1;
                }                                

                srv.Incluir(model);
                srv.Salvar(SessaoUsuario.Sessao.login);

                model.protocolo = model.motoristaid + '/' + DateTime.Now.Year.ToString();
                
                srv.Salvar(SessaoUsuario.Sessao.login);


                if(model.situacaocadastroid == 1)
                {
                    EnviarMensagem("Cadastro em análise. Protocolo Nº: " + model.protocolo, TipoMensagem.Verde);
                }
                else 
                {
                    EnviarMensagem("Cadastro realizado com sucesso. Protocolo Nº: " + model.protocolo, TipoMensagem.Verde);
                }               

                return RedirectToAction("Incluir");
            }
            catch (Exception ex)
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
      
        public JsonResult ObterDetran(string cpf, string cnh)
        {
            var srv = new detranService();
            var obj = srv.GetDadosCNH(Extensao.RemoveMascara(cpf), cnh);

            if (obj == null) 
            {
                return Json(new { Result = "Error"}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Result = "OK", Record = obj }, JsonRequestBehavior.AllowGet);
        }

       

        private void PreparaViewBag()
        {
            SessaoUsuario.Sessao.usuarioid = 1;
            SessaoUsuario.Sessao.login = "MOTORISTA";

            ViewBag.SituacaoLista = ListaSituacao();
        }
    }
}
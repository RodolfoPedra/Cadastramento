using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Mvc.Models;
using Cadastramento.Service;
using Cadastramento.Service.Logic.Cadastramento;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    public class VeiculoController : BaseController
    {
        // GET: Veiculo
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
        public ActionResult Incluir(veiculo model, HttpPostedFileBase file)
        {
            try
            {
                var srv = new BaseService<veiculo>();

                var b = new BinaryReader(file.InputStream);
                model.arquivo = b.ReadBytes(file.ContentLength);
                model.contenttype = file.ContentType;
                model.nomearquivo = file.FileName;


                model.usuarioidinclusao = SessaoUsuario.Sessao.usuarioid;
                model.datahorainclusao = DateTime.Now;
                if (model.situacaocadastroid != 2)
                {
                    model.situacaocadastroid = 1;
                }
                srv.Incluir(model);
                srv.Salvar(SessaoUsuario.Sessao.login);

                model.protocolo = model.veiculoid + '/' + DateTime.Now.Year.ToString();

                srv.Salvar(SessaoUsuario.Sessao.login);

                EnviarMensagem("Operação realizada com sucesso. Protocolo Nº: " + model.protocolo, TipoMensagem.Verde);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                EnviarMensagem("Ocorreu um erro ao tentar realizar a operação desejada.", TipoMensagem.Vermelho);


                return View(model);
            }
        }

        public JsonResult ObterDetran(string placa, string renavam)
        {
            var srv = new detranService();
            var obj = srv.GetDadosVeiculo(placa, renavam);

            return Json(new { Result = "OK", Record = obj }, JsonRequestBehavior.AllowGet);
        }

        private void PreparaViewBag()
        {
            SessaoUsuario.Sessao.usuarioid = 1;
            SessaoUsuario.Sessao.login = "VEÍCULO";

            ViewBag.ListaTipoVeiculo = TipoVeiculo();
            ViewBag.ListaMonitoramento = ListaMonitoramento();
            ViewBag.ListaTipoCarga = TipoCarga();
        }
    }
}
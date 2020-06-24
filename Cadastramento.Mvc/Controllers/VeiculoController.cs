using System;
using System.Collections.Generic;
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

        private void PreparaViewBag()
        {
            ViewBag.ListaTipoVeiculo = TipoVeiculo();
            ViewBag.ListaMonitoramento = ListaMonitoramento();
            ViewBag.ListaTipoCarga = TipoCarga();
        }
    }
}
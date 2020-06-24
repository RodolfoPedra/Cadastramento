using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    public class CarroceriaController : BaseController
    {
        // GET: Carroceria
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir() {

            PreparaViewBag();

            return View();
        }

        private void PreparaViewBag()
        {
            ViewBag.ListaTipoCarroceria = TipoCarroceria();
            ViewBag.ListaMonitoramento = ListaMonitoramento();
            ViewBag.ListaTipoCarga = TipoCarga();
        }
    }
}
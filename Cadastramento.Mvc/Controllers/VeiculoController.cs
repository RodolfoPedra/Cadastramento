﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastramento.Mvc.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {

            return View();
        }
    }
}
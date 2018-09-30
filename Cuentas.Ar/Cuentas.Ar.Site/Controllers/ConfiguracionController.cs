using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class ConfiguracionController : Controller
    {
        public ActionResult AltaCategoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AltaCategoria(Categoria model)
        {
            return View();
        }
    }
}
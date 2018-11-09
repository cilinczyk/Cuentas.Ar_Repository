using System;
using System.Security.Claims;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class RegistroController : Controller
    {
        #region [Región: Listado de Registro]
        public ActionResult Listado()
        {
            ViewData.Model = new RegistroBusiness().Listar();
            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            var listadoRegistro = new RegistroBusiness().Listar();
            return PartialView("_ListaRegistro", listadoRegistro);
        }
        #endregion
    }
}
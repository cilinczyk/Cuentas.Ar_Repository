using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Cuentas.Ar.Business;

namespace Cuentas.Ar.Site.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Listado()
        {
            #region [Región: Actualizar Estados Objetivo
            var objetivoBusiness = new ObjetivoBusiness();

            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            return View("Listado");
        }
    }
}
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
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(idUsuario), "idCategoria", "Descripcion");
            ViewBag.ddl_SubCategoria = new SelectList(new SubCategoriaBusiness().Listar(idUsuario), "idSubCategoria", "Descripcion");
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            ViewBag.ddl_Moneda = new SelectList(new MonedaBusiness().Listar(), "idMoneda", "Descripcion");
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
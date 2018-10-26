using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Cuentas.Ar.Site.Controllers
{
    public class CategoriaController : Controller
    {
        #region [Región: Listado]
        public ActionResult Listado()
        {
            var listadoCategoria = new CategoriaBusiness().Listar();
            return View("Listado", listadoCategoria);
        }
        #endregion

        #region [Región: Alta de categoria]
        public ActionResult Alta()
        {
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            return View("Alta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Categoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    RedirectToAction("Listado");
                }

                ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                return View("Alta", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Editar de categoria]
        public ActionResult Editar()
        {
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            return View("Editar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Editar(Categoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    return View("Detalle", model);
                }

                ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                return View("Editar", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
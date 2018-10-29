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
            ViewData.Model = new CategoriaBusiness().Listar();
            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            var listadoCategoria = new CategoriaBusiness().Listar();
            return PartialView("_ListaCategoria", listadoCategoria);
        }
        #endregion

        #region [Región: Alta de categoria]
        public ActionResult Alta()
        {
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            return PartialView("_Alta");
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
                    new CategoriaBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "Categoria");
                    return Json(new { success = true, url });
                }
                else
                {
                    ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
                    return PartialView("_Alta", model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Edición de categoria]
        public ActionResult Edicion(int idCategoria)
        {
            var model = new CategoriaBusiness().Obtener(idCategoria);

            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            return PartialView("_Edicion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edicion(Categoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new CategoriaBusiness().Modificar(model);

                    string url = Url.Action("ListaParcial", "Categoria");
                    return Json(new { success = true, url });
                }

                ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de categoria]
        public ActionResult Baja(int idCategoria, string descripcion)
        {
            ViewBag.idCategoria = idCategoria;
            ViewBag.Descripcion = descripcion;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult BajaConfirmada(int idCategoria)
        {
            try
            {
                new CategoriaBusiness().Eliminar(idCategoria);

                string url = Url.Action("ListaParcial", "Categoria");
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
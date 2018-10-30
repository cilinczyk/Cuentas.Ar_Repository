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
    public class SubCategoriaController : Controller
    {
        #region [Región: Listado de SubCategoria]
        public ActionResult Listado()
        {
            ViewData.Model = new SubCategoriaBusiness().Listar();
            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            var listadoSubCategoria = new SubCategoriaBusiness().Listar();
            return PartialView("_ListaSubCategoria", listadoSubCategoria);
        }
        #endregion

        #region [Región: Alta de SubCategoria]
        public ActionResult Alta()
        {
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
            return PartialView("_Alta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(SubCategoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new SubCategoriaBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "SubCategoria");
                    return Json(new { success = true, url });
                }
                else
                {
                    ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
                    return PartialView("_Alta", model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Edición de SubCategoria]
        public ActionResult Edicion(int idCategoria)
        {
            var model = new SubCategoriaBusiness().Obtener(idCategoria);

            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
            return PartialView("_Edicion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edicion(SubCategoria model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new SubCategoriaBusiness().Modificar(model);

                    string url = Url.Action("ListaParcial", "SubCategoria");
                    return Json(new { success = true, url });
                }

                ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de SubCategoria]
        public ActionResult Baja(int idSubCategoria, string descripcion)
        {
            ViewBag.idSubCategoria = idSubCategoria;
            ViewBag.Descripcion = descripcion;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Baja(int idSubCategoria)
        {
            try
            {
                new SubCategoriaBusiness().Eliminar(idSubCategoria);

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
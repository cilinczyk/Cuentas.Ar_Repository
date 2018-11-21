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
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewData.Model = new SubCategoriaBusiness().Listar(idUsuario);

            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var listadoSubCategoria = new SubCategoriaBusiness().Listar(idUsuario);

            return PartialView("_ListaSubCategoria", listadoSubCategoria);
        }
        #endregion

        #region [Región: Alta de SubCategoria]
        public ActionResult Alta()
        {
            CargarCombos();
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
                    model.idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    new SubCategoriaBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "SubCategoria");
                    return Json(new { success = true, url });
                }
                else
                {
                    CargarCombos();
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
        public ActionResult Edicion(int idSubCategoria)
        {
            var model = new SubCategoriaBusiness().Obtener(idSubCategoria);

            CargarCombos();
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

                CargarCombos();
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

                string url = Url.Action("ListaParcial", "SubCategoria");
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Auxiliares]
        public JsonResult ListarSubCategorias(int idCategoria)
        {
            try
            {
                if (idCategoria!= 0)
                {
                    int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    List<SubCategoria> ddlSubCategorias = new SubCategoriaBusiness().Listar(idUsuario, idCategoria);

                    if (ddlSubCategorias.Count > 0)
                    {
                        return this.Json(new { Estado = 1, Combo = new SelectList(ddlSubCategorias.ToArray(), "idSubCategoria", "Descripcion") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return this.Json(new { Estado = 0, Mensaje = "No se han encontrado subCategorias para la categoría seleccionada." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return this.Json(new { Estado = 0, Mensaje = "No se ha enviado una categoría." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return this.Json(new { Estado = 0, Mensaje = "Se ha encontrado un error al cargar el listado de subCategorias." }, JsonRequestBehavior.AllowGet);
            }
        }

        private void CargarCombos()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(idUsuario), "idCategoria", "Descripcion");
        }
        #endregion
    }
}
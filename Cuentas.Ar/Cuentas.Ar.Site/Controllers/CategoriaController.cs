using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class CategoriaController : Controller
    {
        #region [Región: Listado de Categoria]
        public ActionResult Listado()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewData.Model = new CategoriaBusiness().Listar(idUsuario);

            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var listadoCategoria = new CategoriaBusiness().Listar(idUsuario);

            return PartialView("_ListaCategoria", listadoCategoria);
        }
        #endregion

        #region [Región: Alta de Categoria]
        public ActionResult Alta()
        {
            CargarCombos();
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
                    model.idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    new CategoriaBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "Categoria");
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

        #region [Región: Edición de Categoria]
        public ActionResult Edicion(int idCategoria)
        {
            var model = new CategoriaBusiness().Obtener(idCategoria);

            CargarCombos();
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

                CargarCombos();
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de Categoria]
        public ActionResult Baja(int idCategoria, string descripcion)
        {
            ViewBag.idCategoria = idCategoria;
            ViewBag.Descripcion = descripcion;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Baja(int idCategoria)
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

        #region [Región: Auxiliares]
        public JsonResult ListarCategorias(int idtipoRegistro)
        {
            try
            {
                if (idtipoRegistro != 0)
                {
                    int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    List<Categoria> ddlCategorias = new CategoriaBusiness().Listar(idUsuario, idtipoRegistro);

                    if (ddlCategorias.Count > 0)
                    {
                        return this.Json(new { Estado = 1, Combo = new SelectList(ddlCategorias.ToArray(), "idCategoria", "Descripcion") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return this.Json(new { Estado = 0, Mensaje = "No se han encontrado categorías para la categoría seleccionada." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return this.Json(new { Estado = 0, Mensaje = "No se ha enviado un tipo de registro." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return this.Json(new { Estado = 0, Mensaje = "Se ha encontrado un error al cargar el listado de categorias." }, JsonRequestBehavior.AllowGet);
            }
        }

        private void CargarCombos()
        {
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
        }
        #endregion
    }
}
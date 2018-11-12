using System;
using System.Collections.Generic;
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
            M_ListadoRegistro model = new M_ListadoRegistro();
            model.FiltroRegistro.idUsuario = idUsuario;
            model.FiltroRegistro.FechaDesde = DateTime.Now.AddMonths(-1);
            model.FiltroRegistro.FechaHasta = DateTime.Now;
            model.ListaRegistro = new RegistroBusiness().Listar(model.FiltroRegistro);

            CargarCombosListado(idUsuario);
            return View("Listado", model);
        }

        public ActionResult ListaParcial()
        {
            M_FiltroRegistro filtroRegistro = Session["FiltroRegistro"] as M_FiltroRegistro;
            var listadoRegistro = new RegistroBusiness().Listar(filtroRegistro);

            return PartialView("_ListaRegistro", listadoRegistro);
        }
        #endregion

        #region [Región: Búsqueda]
        public ActionResult Buscar(M_FiltroRegistro filtroRegistro)
        {
            Session["FiltroRegistro"] = filtroRegistro;
            return RedirectToAction("ListaParcial", "Registro");
        }
        #endregion

        #region [Región: Alta de Registro]
        public ActionResult Alta()
        {
            CargarCombos();
            return PartialView("_Alta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Registro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    new RegistroBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "Registro");
                    return Json(new { success = true, url });
                }
                else
                {
                    CargarCombos(model.idTipoRegistro, model.idCategoria);
                    return PartialView("_Alta", model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Edición de Registro]
        public ActionResult Edicion(int idRegistro)
        {
            var model = new RegistroBusiness().Obtener(idRegistro);

            CargarCombos(model.idTipoRegistro, model.idCategoria);
            return PartialView("_Edicion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edicion(Registro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new RegistroBusiness().Modificar(model);

                    string url = Url.Action("ListaParcial", "Registro");
                    return Json(new { success = true, url });
                }

                CargarCombos(model.idTipoRegistro, model.idCategoria);
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de Registro]
        public ActionResult Baja(int idRegistro, string tipoRegistro, decimal importe)
        {
            ViewBag.idRegistro = idRegistro;
            ViewBag.TipoRegistro = tipoRegistro;
            ViewBag.Importe = importe;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Baja(int idRegistro)
        {
            try
            {
                new RegistroBusiness().Eliminar(idRegistro);

                string url = Url.Action("ListaParcial", "Registro");
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Detalle de Registro]
        public ActionResult Detalle(int idRegistro)
        {
            var model = new RegistroBusiness().ObtenerCompleto(idRegistro);
            return PartialView("_Detalle", model);
        }
        #endregion

        #region [Región: Auxiliares]
        private void CargarCombosListado(int idUsuario)
        {
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(idUsuario), "idCategoria", "Descripcion");
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            ViewBag.ddl_Moneda = new SelectList(new MonedaBusiness().Listar(), "idMoneda", "Descripcion");
        }

        private void CargarCombos(int? idTipoRegistro = null, int? idCategoria = null)
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewBag.ddl_TipoRegistro = new SelectList(new TipoRegistroBusiness().Listar(), "idTipoRegistro", "Descripcion");
            ViewBag.ddl_Moneda = new SelectList(new MonedaBusiness().Listar(), "idMoneda", "Descripcion");

            if (idCategoria.HasValue)
            {
                ViewBag.ddl_SubCategoria = new SelectList(new SubCategoriaBusiness().Listar(idCategoria.Value), "idSubCategoria", "Descripcion");
            }
            else
            {
                List<SelectListItem> listaVacia = new List<SelectListItem>
                {
                    new SelectListItem() { Text = string.Empty, Value = string.Empty }
                };

                ViewBag.ddl_SubCategoria = new SelectList(listaVacia);
            }

            if (idTipoRegistro.HasValue)
            {
                ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(idUsuario, idTipoRegistro.Value), "idCategoria", "Descripcion");
            }
            else
            {
                List<SelectListItem> listaVacia = new List<SelectListItem>
                {
                    new SelectListItem() { Text = string.Empty, Value = string.Empty }
                };

                ViewBag.ddl_Categoria = new SelectList(listaVacia);
            }
        }
        #endregion
    }
}
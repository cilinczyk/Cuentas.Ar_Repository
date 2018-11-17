using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Site.Helpers;

namespace Cuentas.Ar.Site.Controllers
{
    public class ObjetivoController : Controller
    {
        #region [Región: Listado de Objetivo]
        public ActionResult Listado()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            var objetivoBusiness = new ObjetivoBusiness();

            #region [Región: Actualizar Estados]
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            M_ListadoObjetivo model = new M_ListadoObjetivo();
            model.FiltroObjetivo.idUsuario = idUsuario;
            model.FiltroObjetivo.FechaDesde = DateTime.Now.AddYears(-1);
            model.FiltroObjetivo.FechaHasta = DateTime.Now.AddYears(1);
            model.ListaObjetivo = objetivoBusiness.Listar(model.FiltroObjetivo);

            Session["FiltroObjetivo"] = model.FiltroObjetivo;
            CargarCombos();
            return View("Listado", model);
        }

        public ActionResult ListaParcial()
        {
            M_FiltroObjetivo filtroObjetivo = Session["FiltroObjetivo"] as M_FiltroObjetivo;
            var listadoObjetivo = new ObjetivoBusiness().Listar(filtroObjetivo);

            return PartialView("_ListaObjetivo", listadoObjetivo);
        }
        #endregion

        #region [Región: Búsqueda]
        public ActionResult Buscar(M_FiltroObjetivo filtroObjetivo)
        {
            Session["FiltroObjetivo"] = filtroObjetivo;
            return RedirectToAction("ListaParcial", "Objetivo");
        }
        #endregion

        #region [Región: Alta de Objetivo]
        public ActionResult Alta()
        {
            CargarCombos();
            return PartialView("_Alta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Objetivo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                    var objetivoBusiness = new ObjetivoBusiness();

                    #region [Región: Alta de Objetivo]
                    model.idUsuario = idUsuario;
                    model.idEstadoObjetivo = ObjetivoHelper.ObtenerEstadoObjetivo(model);

                    objetivoBusiness.Guardar(model);
                    #endregion

                    string url = Url.Action("ListaParcial", "Objetivo");
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

        #region [Región: Edición de Objetivo]
        public ActionResult Edicion(int idObjetivo)
        {
            var model = new ObjetivoBusiness().Obtener(idObjetivo);

            CargarCombos();
            return PartialView("_Edicion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edicion(Objetivo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region [Región: Edición de Objetivo]
                    var objetivoBusiness = new ObjetivoBusiness();
                    model.idEstadoObjetivo = ObjetivoHelper.ObtenerEstadoObjetivo(model);

                    objetivoBusiness.Modificar(model);
                    #endregion

                    string url = Url.Action("ListaParcial", "Objetivo");
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

        #region [Región: Baja de Objetivo]
        public ActionResult Baja(int idObjetivo, string motivo, decimal importe)
        {
            ViewBag.idObjetivo = idObjetivo;
            ViewBag.Motivo = motivo;
            ViewBag.Importe = importe;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Baja(int idObjetivo)
        {
            try
            {
                new ObjetivoBusiness().Eliminar(idObjetivo);

                string url = Url.Action("ListaParcial", "Objetivo");
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Detalle de Objetivo]
        public ActionResult Detalle(int idObjetivo)
        {
            var model = new ObjetivoBusiness().ObtenerCompleto(idObjetivo);
            return PartialView("_Detalle", model);
        }
        #endregion

        #region [Región: Auxiliares]
        private void CargarCombos()
        {
            ViewBag.ddl_EstadoObjetivo = new SelectList(new EstadoObjetivoBusiness().Listar(), "idEstadoObjetivo", "Descripcion");
            ViewBag.ddl_Moneda = new SelectList(new MonedaBusiness().Listar(), "idMoneda", "Descripcion");
        }
        #endregion
    }
}
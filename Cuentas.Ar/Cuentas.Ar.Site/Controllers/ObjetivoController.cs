using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class ObjetivoController : Controller
    {
        #region [Región: Listado de Objetivo]
        public ActionResult Listado()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            M_ListadoObjetivo model = new M_ListadoObjetivo();
            model.FiltroObjetivo.idUsuario = idUsuario;
            model.FiltroObjetivo.FechaDesde = DateTime.Now.AddYears(-1);
            model.FiltroObjetivo.FechaHasta = DateTime.Now.AddYears(1);
            model.ListaObjetivo = new ObjetivoBusiness().Listar(model.FiltroObjetivo);

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
                var idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
                var objetivoBusiness = new ObjetivoBusiness();

                #region [Región: Validaciones]
                if (objetivoBusiness.ListarObjetivos(idUsuario, model.idMoneda).Any(x => x.Fecha >= model.Fecha && x.idEstadoObjetivo != eEstadoObjetivo.Pendiente && x.idEstadoObjetivo != eEstadoObjetivo.Finalizado))
                {
                    ModelState.AddModelError("Objetivo", "Ya contiene un objetivo activo dentro de la fecha seleccionada.");
                }
                #endregion
                if (ModelState.IsValid)
                {
                    #region [Región: Alta de Objetivo]
                    model.idUsuario = idUsuario;
                    model.idEstadoObjetivo = ObtenerEstadoObjetivo(model);

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

        private int ObtenerEstadoObjetivo(Objetivo objetivo)
        {
            var objetivoBusiness = new ObjetivoBusiness();

            #region [Región: Estado]
            var usuarioBusiness = new UsuarioBusiness();
            decimal capAhorro = 0;
            int mesesRestantes = (int)MonthDifference(objetivo.Fecha, DateTime.Now);

            decimal unCuarto = (objetivo.Importe * 25 / 100);
            decimal dosCuartos = (objetivo.Importe * 50 / 100);
            decimal tresCuartos = (objetivo.Importe * 75 / 100);

            if (objetivo.idMoneda == eMoneda.Pesos)
            {
                capAhorro = usuarioBusiness.Obtener(objetivo.idUsuario).CapacidadAhorroPesos * mesesRestantes;
            }
            else
            {
                capAhorro = usuarioBusiness.Obtener(objetivo.idUsuario).CapacidadAhorroDolares * mesesRestantes;
            }

            if (capAhorro >= 0 && capAhorro <= unCuarto)
            {
                return eEstadoObjetivo.Imposible;
            }
            else if (capAhorro > unCuarto && capAhorro <= dosCuartos)
            {
                return eEstadoObjetivo.Complicado;
            }
            else if (capAhorro > dosCuartos && capAhorro <= tresCuartos)
            {
                return eEstadoObjetivo.Posible;
            }
            else
            {
                return eEstadoObjetivo.Excelente;
            }
            #endregion
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
                var objetivoBusiness = new ObjetivoBusiness();

                #region [Región: Validaciones]
                if (objetivoBusiness.ListarObjetivos(model.idUsuario, model.idMoneda).Any(x => x.Fecha >= model.Fecha && x.idEstadoObjetivo != eEstadoObjetivo.Pendiente && x.idEstadoObjetivo != eEstadoObjetivo.Finalizado && x.idObjetivo != model.idObjetivo))
                {
                    ModelState.AddModelError("Objetivo", "Ya contiene un objetivo activo dentro de la fecha seleccionada.");
                }
                #endregion

                if (ModelState.IsValid)
                {
                    #region [Región: Edición de Objetivo]
                    model.idEstadoObjetivo = ObtenerEstadoObjetivo(model);
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
        public static decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }

        private void CargarCombos()
        {
            ViewBag.ddl_EstadoObjetivo = new SelectList(new EstadoObjetivoBusiness().Listar(), "idEstadoObjetivo", "Descripcion");
            ViewBag.ddl_Moneda = new SelectList(new MonedaBusiness().Listar(), "idMoneda", "Descripcion");
        }
        #endregion
    }
}
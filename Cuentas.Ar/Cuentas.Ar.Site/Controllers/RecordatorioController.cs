using System;
using System.Web.Mvc;
using Cuentas.Ar.Business;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class RecordatorioController : Controller
    {
        #region [Región: Listado de Recordatorio]
        public ActionResult Listado()
        {
            ViewData.Model = new RecordatorioBusiness().Listar();
            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            var listadoRecordatorio = new RecordatorioBusiness().Listar();
            return PartialView("_ListaRecordatorio", listadoRecordatorio);
        }
        #endregion

        #region [Región: Alta de Recordatorio]
        public ActionResult Alta()
        {
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
            ViewBag.ddl_EstadoRecordatorio = new SelectList(new EstadoRecordatorioBusiness().Listar(), "idEstadoRecordatorio", "Descripcion");
            return PartialView("_Alta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Recordatorio model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new RecordatorioBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "Recordatorio");
                    return Json(new { success = true, url });
                }
                else
                {
                    ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
                    ViewBag.ddl_EstadoRecordatorio = new SelectList(new EstadoRecordatorioBusiness().Listar(), "idEstadoRecordatorio", "Descripcion");
                    return PartialView("_Alta", model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Edición de Recordatorio]
        public ActionResult Edicion(int idRecordatorio)
        {
            var model = new RecordatorioBusiness().Obtener(idRecordatorio);

            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
            ViewBag.ddl_EstadoRecordatorio = new SelectList(new EstadoRecordatorioBusiness().Listar(), "idEstadoRecordatorio", "Descripcion");
            return PartialView("_Edicion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edicion(Recordatorio model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new RecordatorioBusiness().Modificar(model);

                    string url = Url.Action("ListaParcial", "Recordatorio");
                    return Json(new { success = true, url });
                }

                ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(), "idCategoria", "Descripcion");
                ViewBag.ddl_EstadoRecordatorio = new SelectList(new EstadoRecordatorioBusiness().Listar(), "idEstadoRecordatorio", "Descripcion");
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de Recordatorio]
        public ActionResult Baja(int idRecordatorio, string titulo)
        {
            ViewBag.idRecordatorio = idRecordatorio;
            ViewBag.Titulo = titulo;

            return PartialView("_Baja");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Baja(int idRecordatorio)
        {
            try
            {
                new RecordatorioBusiness().Eliminar(idRecordatorio);

                string url = Url.Action("ListaParcial", "Recordatorio");
                return Json(new { success = true, url });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Alta de Recordatorio]
        public ActionResult Detalle(int idRecordatorio)
        {
            var model = new RecordatorioBusiness().ObtenerCompleto(idRecordatorio);
            return PartialView("_Detalle", model);
        }
        #endregion
    }
}
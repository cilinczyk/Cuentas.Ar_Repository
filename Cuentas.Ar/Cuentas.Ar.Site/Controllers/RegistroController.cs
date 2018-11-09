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
            ViewData.Model = new RegistroBusiness().Listar();
            return View("Listado");
        }

        public ActionResult ListaParcial()
        {
            var listadoRegistro = new RegistroBusiness().Listar();
            return PartialView("_ListaRegistro", listadoRegistro);
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
                    new RegistroBusiness().Guardar(model);

                    string url = Url.Action("ListaParcial", "Registro");
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

        #region [Región: Edición de Registro]
        public ActionResult Edicion(int idRegistro)
        {
            var model = new RegistroBusiness().Obtener(idRegistro);

            CargarCombos();
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

                CargarCombos();
                return PartialView("_Edicion", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Baja de Registro]
        public ActionResult Baja(int idRegistro, string titulo)
        {
            ViewBag.idRegistro = idRegistro;
            ViewBag.Titulo = titulo;

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

        private void CargarCombos()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            ViewBag.ddl_Categoria = new SelectList(new CategoriaBusiness().Listar(idUsuario), "idCategoria", "Descripcion");
            ViewBag.ddl_EstadoRegistro = new SelectList(new EstadoRegistroBusiness().Listar(), "idEstadoRegistro", "Descripcion");
        }
    }
}
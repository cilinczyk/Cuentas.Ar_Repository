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
    public class UsuarioController : Controller
    {
        #region [Región: Alta de usuario]

        #region [Región: Paso 1 (Seleccionar Tipo de Cuenta)]
        [AllowAnonymous]
        public ActionResult AltaLoginTipoCuenta()
        {
            return View("AltaLoginTipoCuenta");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AltaLoginTipoCuenta(int idTipoCuenta)
        {
            try
            {
                return RedirectToAction("AltaLogin", "Usuario", new { idTipoCuenta });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Paso 2 (Datos de la cuenta)]
        [AllowAnonymous]
        public ActionResult AltaLogin(int idTipoCuenta)
        {
            M_UsuarioLogin model = new M_UsuarioLogin
            {
                idTipoCuenta = idTipoCuenta
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(M_UsuarioLogin model)
        {
            try
            {
                UsuarioBusiness usuarioBusiness = new UsuarioBusiness();

                if (usuarioBusiness.ValidarEmail(model.DatosBasicos.Email))
                {
                    ModelState.AddModelError("UsuarioRegistrado", "El mail ingresado ya se encuentra registrado.");
                }

                if (!ModelState.ContainsKey("UsuarioRegistrado"))
                {
                    if (model.idTipoCuenta == eTipoCuenta.Free)
                    {
                        AltaUsuario(model);
                        return View("AltaLoginResult", model);
                    }
                    else
                    {
                        ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                        return View("AltaLoginFormaPago", model);
                    }
                }

                return View("AltaLogin", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Paso 3 (Forma de Pago)]
        [AllowAnonymous]
        public ActionResult AltaLoginFormaPago(M_UsuarioLogin model)
        {
            ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
            return View("AltaLoginFormaPago", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AltaLoginFormaPagoConfirm(M_UsuarioLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AltaUsuario(model, fechaCobro: DateTime.Now.AddMonths(1));
                    return View("AltaLoginResult", model);
                }

                ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                return View("AltaLoginFormaPago", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Región: Paso 4 (Resultado)]
        [AllowAnonymous]
        public ActionResult AltaLoginResult(M_UsuarioLogin usuario)
        {
            return View("AltaLoginResult", usuario);
        }
        #endregion

        private static void AltaUsuario(M_UsuarioLogin model, DateTime? fechaCobro = null)
        {
            UsuarioBusiness usuarioBusiness = new UsuarioBusiness();

            #region Alta de Usuario
            Usuario usuario = new Usuario
            {
                Estado = true,
                FechaAlta = DateTime.Now,
                idTipoCuenta = model.idTipoCuenta,
                Nombre = model.DatosBasicos.Nombre,
                Email = model.DatosBasicos.Email,
                Password = Crypto.SHA1(model.DatosBasicos.Password),

                idTipoTarjeta = model.FormaPago.idTipoTarjeta,
                NroTarjeta = model.FormaPago.NroTarjeta,
                CodSeguridad = model.FormaPago.CodSeguridad,
                VencTarjeta = model.FormaPago.VencTarjeta,
                FechaCobro = fechaCobro
            };

            usuarioBusiness.Guardar(usuario);
            #endregion
        }
        #endregion

        #region [Región: Mis Datos]
        [AllowAnonymous]
        public ActionResult MisDatos()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);
            Usuario usuario = new UsuarioBusiness().Obtener(idUsuario);

            ViewBag.ddl_Provincia = new SelectList(new ProvinciaBusiness().Listar(), "idProvincia", "Descripcion");
            ViewBag.ddl_TipoCuenta = new SelectList(new TipoCuentaBusiness().Listar(), "idTipoCuenta", "Descripcion");
            ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
            return View("MisDatos", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult MisDatos(Usuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioBusiness usuarioBusiness = new UsuarioBusiness();
                    var usuarioOriginal = usuarioBusiness.Obtener(model.idUsuario);

                    if (model.Password != "password")
                    {
                        model.Password = Crypto.SHA1(model.Password);
                    }
                    else
                    {
                        model.Password = usuarioOriginal.Password;
                    }

                    if (usuarioOriginal.idTipoCuenta == eTipoCuenta.Free)
                    {
                        model.FechaCobro = DateTime.Now.AddMonths(1);
                    }

                    usuarioBusiness.Modificar(model);
                    model.UsuarioActualizado = true;
                }

                ViewBag.ddl_Provincia = new SelectList(new ProvinciaBusiness().Listar(), "idProvincia", "Descripcion");
                ViewBag.ddl_TipoCuenta = new SelectList(new TipoCuentaBusiness().Listar(), "idTipoCuenta", "Descripcion");
                ViewBag.ddl_TipoTarjeta = new SelectList(new TipoTarjetaBusiness().Listar(), "idTipoTarjeta", "Descripcion");
                return RedirectToAction("MisDatos", "Usuario", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
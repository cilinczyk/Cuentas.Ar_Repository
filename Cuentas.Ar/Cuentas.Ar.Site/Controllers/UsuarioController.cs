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
                return RedirectToAction("AltaLogin", "Usuario", new { idTipoCuenta } );

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
            Usuario model = new Usuario
            {
                Administrador = false,
                idTipoCuenta = idTipoCuenta
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Usuario model)
        {
            try
            {
                UsuarioBusiness usuarioBusiness = new UsuarioBusiness();
                if (usuarioBusiness.ValidarEmail(model.Email))
                {
                    ModelState.AddModelError("UsuarioRegistrado", "El mail ingresado ya se encuentra registrado.");
                }

                if (ModelState.IsValid)
                {
                    int id_user = 0;

                    #region Alta de Usuario
                    model.Password = Crypto.SHA1(model.Password);
                    model.Estado = true;
                    model.FechaAlta = DateTime.Now;

                    id_user = usuarioBusiness.Guardar(model);
                    #endregion

                    return RedirectToAction("AltaLoginResult", "Usuario", model);
                }

                return View("AltaLogin", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        [AllowAnonymous]
        public ActionResult AltaLoginResult(Usuario user)
        {
            return View("AltaLoginResult", user);
        }
        #endregion
    }
}
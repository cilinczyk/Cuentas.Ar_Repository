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
    public class CuentaController : Controller
    {
        #region [Región: Login]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string mensaje)
        {
            //Si el usuario ya se encuentra logueado, redirecciono al home.
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("Index", "Home");
            }

            //Si no se encuentra logueado, muestro el formulario de login.
            ViewBag.ReturnUrl = returnUrl;

            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Mensaje = mensaje;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(M_Usuario model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid) //Verificar que el modelo de datos sea válido en cuanto a la definición de las propiedades
                {
                    //Valido si el usuario existe.
                    UsuarioBusiness usuarioBusiness = new UsuarioBusiness();

                    //Valido si existe el mail de usuario
                    if (usuarioBusiness.ValidarEmail(model.Mail))
                    {
                        //Si el usuario existe, cargo los datos en sesion.
                        Usuario usuario = usuarioBusiness.IniciarSesion(model.Mail, Crypto.SHA1(model.Password));
                        if (usuario != null)
                        {
                            #region [Región: Usuario existente]

                            #region [Región: Seteo las claims]

                            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.GivenName, usuario.Nombre),
                                new Claim(ClaimTypes.Email, usuario.Email),
                                new Claim(ClaimTypes.Sid, usuario.idUsuario.ToString()),
                                new Claim(ClaimTypes.Role, usuario.Administrador ? "ADMIN" : "STANDARD"),
                            };

                            #endregion

                            #region [Región: Guardo el usuario en session]

                            var identidad = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = model.Recordarme }, identidad);
                            
                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region [Región: Usuario incorrecto]

                            ModelState.AddModelError("EstadoLogin", "La contraseña ingresada es incorrecta.");

                            #endregion
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("EstadoLogin", "Las credenciales ingresadas son incorrectas o el usuario se encuentra bloqueado.");
                    }
                }

                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Region: LogOff / LogOut]

        public ActionResult LogOff()
        {
            try
            {
                //Borro las cookies del usuario logueado.
                HttpContext.GetOwinContext().Authentication.SignOut();

                //Borro la sesión.
                Session.Clear();
                Session.Abandon();

                //Redirecciono al login.
                return RedirectToAction("Login", "Cuenta");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public ActionResult TimeOut()
        {
            try
            {
                //Borro las cookies del usuario logueado.
                HttpContext.GetOwinContext().Authentication.SignOut();

                //Borro la sesión.
                Session.Clear();
                Session.Abandon();

                //Redirecciono a la vista de TimeOut.
                return View("TimeOut");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public ActionResult UsuarioLogout()
        {
            //Borro las cookies del usuario logueado.
            HttpContext.GetOwinContext().Authentication.SignOut();

            //Borro la sesión.
            Session.Clear();
            Session.Abandon();

            //Redirecciono a la vista de UsuarioLogout.
            return View("UsuarioLogout");
        }
        
        #endregion

        #region [Region: Funciones Auxiliares]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
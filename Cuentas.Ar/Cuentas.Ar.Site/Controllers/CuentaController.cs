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
        #region MyRegion
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //Si el usuario ya se encuentra logueado, redirecciono al home.
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            //Si no se encuentra logueado, muestro el formulario de login.
            ViewBag.ReturnUrl = returnUrl;
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
                            Guid userToken = Guid.NewGuid();

                            #region [Región: Usuario existente]

                            #region [Región: Seteo las claims]

                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.GivenName, usuario.Nombre),
                                new Claim(ClaimTypes.Email, usuario.Email),
                                new Claim(ClaimTypes.Sid, usuario.idUsuario.ToString()),
                                new Claim("http://example.org/claims/UserToken", userToken.ToString())
                            };

                            #endregion

                            #region [Región: Guardo el usuario en session]

                            var identidad = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.Recordarme }, identidad);

                            Session.Add("UserToken", userToken);

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}

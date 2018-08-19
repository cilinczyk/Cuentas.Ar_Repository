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
        [AllowAnonymous]
        public ActionResult AltaLogin()
        {
            var usuario = new Usuario();
            usuario.Administrador = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Alta(Usuario model, bool login = false)
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
                    
                    id_user = usuarioBusiness.Guardar(model);
                    #endregion

                    if (login)
                    {
                        return RedirectToAction("Login", "Cuenta", new { mensaje = "¡Cuenta creada satisfactoriamente!" });
                    }
                    else
                    {
                        return RedirectToAction("Detalle", "Usuario", new { @id = id_user });
                    }
                }

                return View("Alta/Alta", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
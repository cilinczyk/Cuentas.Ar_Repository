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
    }
}
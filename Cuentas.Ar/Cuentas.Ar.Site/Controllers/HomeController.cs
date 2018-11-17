﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Cuentas.Ar.Business;

namespace Cuentas.Ar.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(ClaimTypes.Sid).Value);

            #region [Región: Actualizar Estados Objetivo
            var objetivoBusiness = new ObjetivoBusiness();
            objetivoBusiness.ActualizarEstados(idUsuario);
            #endregion

            return View();
        }
    }
}
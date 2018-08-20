using System;
using System.Web.Mvc;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Site.Controllers
{
    public class BotoneraController : Controller
    {
        public ActionResult Top()
        {
            try
            {
                #region [Región: Declaraciones]
                BotoneraTop botoneraTop = new BotoneraTop();
                #endregion

                return PartialView("_BotoneraTop", botoneraTop);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
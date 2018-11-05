using System.Globalization;
using System.Security.Claims;
using System.Threading;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cuentas.Ar.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
        }

        protected void Application_BeginRequest()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-AR");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-AR");
        }
    }
}

﻿using System.Web.Optimization;

namespace Cuentas.Ar.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/CuentasArStyleSheet.css",
                "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/vendor/metisMenu/metisMenu.min.css",
                "~/Content/dist/css/sb-admin-2.css",
                "~/Content/vendor/morrisjs/morris.css",
                "~/Content/vendor/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/vendor/jquery/jquery.min.js",
                "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                "~/Content/vendor/metisMenu/metisMenu.min.js",
                "~/Content/vendor/raphael/raphael.min.js",
                "~/Content/vendor/morrisjs/morris.min.js",
                "~/Content/data/morris-data.js",
                "~/Content/dist/js/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate*",
            "~/Scripts/jquery.unobtrusive-ajax.min.js",
            "~/Scripts/mvcfoolproof.unobtrusive.min.js",
            "~/Scripts/MvcFoolproofJQueryValidation.min.js"));
        }
    }
}

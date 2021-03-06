﻿using System.Web.Optimization;

namespace Cuentas.Ar.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region [Región: Login]
            bundles.Add(new StyleBundle("~/bundles/cssLogin").Include(
                "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/cuentaLogin/cuentaLogin.css",
                "~/Content/select2.min.css",
                "~/Content/login/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/Content/login/fonts/iconic/css/material-design-iconic-font.min.css",
                "~/Content/login/css/main.css",
                "~/Content/login/css/util.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jsLogin").Include(
                "~/Scripts/jquery-3.3.1.js",
                "~/Scripts/select2.min.js"));
            #endregion

            #region [Región: Sistema]
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                   "~/Content/CuentasArStyleSheet.css",
                   "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                   "~/Content/assets/css/material-dashboard.min.css",
                   "~/Content/login/fonts/font-awesome-4.7.0/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                    "~/Scripts/jquery-ui.min.js",
                    "~/Scripts/moment_es.js",
                    "~/Content/assets/js/core/popper.min.js",
                    "~/Content/assets/js/core/bootstrap-material-design.min.js",
                    "~/Content/assets/js/plugins/perfect-scrollbar.jquery.min.js",
                    "~/Content/assets/js/material-dashboard.min.js",
                    "~/Content/vendor/bootstrap/js/bootstrap-selectpicker.js"
                    ));
            #endregion

            #region [Región: Auxiliares]
            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                    "~/Scripts/inputmask/inputmask.min.js",
                    "~/Scripts/inputmask/jquery.inputmask.min.js",
                    "~/Scripts/inputmask/inputmask.extensions.min.js",
                    "~/Scripts/inputmask/inputmask.date.extensions.min.js",
                    "~/Scripts/inputmask/inputmask.numeric.extensions.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/moment.min.js",
                "~/Content/assets/js/plugins/bootstrap-datetimepicker.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/mvcfoolproof.unobtrusive.min.js",
                "~/Scripts/MvcFoolproofJQueryValidation.min.js"));
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cuentas.Ar.Site.Helpers
{
    public static class MenuHelper
    {
        private static List<string> controllersByGroups = new List<string>
        {
            "Configuracion"
        };

        public static MvcHtmlString MenuActionLink(this HtmlHelper helper, string label, string actionName, string controllerName, string icon, string title, object routeValues = null, bool marcame = false)
        {
            TagBuilder tagBuilder;
            UrlHelper urlHelper;

            var htmlAttributes = new RouteValueDictionary();
            var routeData = helper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            if (string.Equals(currentAction, actionName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentController, controllerName, StringComparison.OrdinalIgnoreCase))
            {
                htmlAttributes.Add("class", "active");
            }
            else
            {
                if (marcame)
                {
                    htmlAttributes.Add("class", "active");
                }
            }

            htmlAttributes.Add("data-toggle", "tooltip");
            htmlAttributes.Add("data-placement", "right");
            htmlAttributes.Add("title", title);

            var li = new TagBuilder("li");
            var ico = new TagBuilder("i");
            var span = new TagBuilder("span");

            if (!icon.Contains("far "))
            {
                icon = "fa " + icon;
            }

            ico.AddCssClass(icon + " fa-fw");

            urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            tagBuilder = new TagBuilder("a");

            span.InnerHtml = label;

            tagBuilder.InnerHtml = ico.ToString() + " " + span.ToString();

            tagBuilder.Attributes["href"] = urlHelper.Action(actionName, controllerName, routeValues);

            tagBuilder.MergeAttributes(htmlAttributes);
            li.InnerHtml = tagBuilder.ToString();

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuActionLinkMenu(this HtmlHelper helper, string label, string actionName, string controllerName, string icon, bool marcame = false)
        {
            TagBuilder tagBuilder;
            UrlHelper urlHelper;

            var htmlAttributes = new RouteValueDictionary();
            var routeData = helper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            var li = new TagBuilder("li");
            li.AddCssClass("nav-item");

            var tagP = new TagBuilder("p");
            tagP.InnerHtml = label;

            var ico = new TagBuilder("i");
            if (!string.IsNullOrEmpty(icon))
            {
                ico.AddCssClass("fa fa-" + icon);
            }

            if ((string.Equals(currentAction, actionName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentController, controllerName, StringComparison.OrdinalIgnoreCase)) || marcame)
            {
                li.AddCssClass("active");
            }

            urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            tagBuilder = new TagBuilder("a");
            tagBuilder.AddCssClass("nav-link");
            tagBuilder.InnerHtml = ico.ToString() + " " + tagP.ToString();
            tagBuilder.Attributes["href"] = urlHelper.Action(actionName, controllerName);
            tagBuilder.MergeAttributes(htmlAttributes);

            li.InnerHtml = tagBuilder.ToString();

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuActionSubLinkMenu(this HtmlHelper helper, string label, string actionName, string controllerName, string icon, bool marcame = false)
        {
            TagBuilder tagBuilder;
            UrlHelper urlHelper;

            var htmlAttributes = new RouteValueDictionary();
            var routeData = helper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");

            var li = new TagBuilder("li");
            li.AddCssClass("nav-item");

            var tagSpan = new TagBuilder("span");
            tagSpan.InnerHtml = label;

            var ico = new TagBuilder("i");
            if (!string.IsNullOrEmpty(icon))
            {
                ico.AddCssClass("fa fa-" + icon);
                ico.Attributes["style"] = "margin-left: 10%";
            }

            if ((string.Equals(currentAction, actionName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentController, controllerName, StringComparison.OrdinalIgnoreCase)) || marcame)
            {
                li.AddCssClass("active");
            }

            urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            tagBuilder = new TagBuilder("a");
            tagBuilder.AddCssClass("nav-link");
            tagBuilder.InnerHtml = ico.ToString() + " " + tagSpan.ToString();
            tagBuilder.Attributes["href"] = urlHelper.Action(actionName, controllerName);
            tagBuilder.MergeAttributes(htmlAttributes);

            li.InnerHtml = tagBuilder.ToString();

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper, string text, string title, string action, string controller, object routeValues = null, object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["data-toggle"] = "tooltip";
            builder.Attributes["data-placement"] = "bottom";
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString() + Environment.NewLine);
        }

        public static string EstadoMenu(string controlador)
        {
            return controllersByGroups.Select(x => x).Contains(controlador) ? "active" : string.Empty;
        }

        public static string[] ObtenerSaludo()
        {
            int hora = DateTime.Now.TimeOfDay.Hours;
            string[] saludo = new string[2];

            if (hora < 13)
            {
                saludo[0] = "Buenos Días";
                saludo[1] = "fa fa-sun-o right";
            }
            else if (hora >= 13 && hora < 19)
            {
                saludo[0] = "Buenas Tardes";
                saludo[1] = "fa fa-cloud right";
            }
            else
            {
                saludo[0] = "Buenas Noches";
                saludo[1] = "fa fa-moon-o right";
            }

            return saludo;
        }
    }
}
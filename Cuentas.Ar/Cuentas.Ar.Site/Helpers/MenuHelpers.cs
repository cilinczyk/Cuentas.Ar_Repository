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
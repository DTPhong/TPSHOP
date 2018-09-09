using System.Web.Mvc;
using System.Web.Routing;

namespace TPshop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "Login",
                 url: "login.html",
                 defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "HomePage",
                 url: "homepage.html",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "About",
                 url: "about.html",
                 defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "History",
                 url: "history.html",
                 defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "Contact",
                 url: "contact.html",
                 defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "Register",
                 url: "register.html",
                 defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "Search",
                 url: "search.html",
                 defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "Checkout",
                 url: "checkout.html",
                 defaults: new { controller = "ShoppingCart", action = "Checkout", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "Product",
                 url: "{alias}.c-{id}.html",
                 defaults: new { controller = "Product", action = "ProductByCategory", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "ProductGroup",
                 url: "cg-{id}.html",
                 defaults: new { controller = "Product", action = "ProductByGroupCategory", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                 name: "ProductDetail",
                 url: "{alias}.p-{id}.html",
                 defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "TPshop.Web.Controllers" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new string[] { "TPshop.Web.Controllers" }
            );
        }
    }
}
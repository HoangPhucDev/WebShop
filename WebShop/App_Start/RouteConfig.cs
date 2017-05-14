using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Detail",
                url: "Chi-Tiet/{Id}",
                defaults: new { controller = "Detail", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Group Product",
                url: "Nhom/{IdGroup}-{NameGroup}",
                defaults: new { controller = "Home", action = "GroupProduct", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Bakset",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Basket", id = UrlParameter.Optional }
            );



        }
    }
}

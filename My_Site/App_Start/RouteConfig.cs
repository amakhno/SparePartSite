using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace My_Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "SparePart", action = "List" }
            );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "SparePart", action = "List" },
                new { page = @"\d+" }
            );*/

            /*"Edit", "Admin", new { spareId = Model.Id }, FormMethod.Post)
            routes.MapRoute(
                null,
                "{controller}/{action}/{spareId}",
                new { controller = "Admin", action = "Edit" });*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

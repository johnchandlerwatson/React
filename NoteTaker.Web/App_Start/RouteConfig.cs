using System.Web.Mvc;
using System.Web.Routing;

namespace NoteTaker.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewNote",
                url: "notes/new",
                defaults: new { controller = "Home", action = "AddNote" }
            );

            routes.MapRoute(
                name: "Notes",
                url: "notes",
                defaults: new { controller = "Home", action = "Notes" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

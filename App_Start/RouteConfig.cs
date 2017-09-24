using System.Web.Mvc;
using System.Web.Routing;

namespace BuilderWebApp3 {
    public static class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "HomePage",
               url: "",
               defaults: new { controller = "Home", action = "GoToHome", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "SessionPage",
               url: "Session/Add",
               defaults: new { controller = "Session", action = "AddSessionInfo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "SessionEndTimePage",
               url: "Session/AddSessionEndTime",
               defaults: new { controller = "Session", action = "AddSessionEndTime", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminPortal",
                url: "Index/Admin",
                defaults: new {controller = "Home", action = "GoToRegisterNewUser", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}

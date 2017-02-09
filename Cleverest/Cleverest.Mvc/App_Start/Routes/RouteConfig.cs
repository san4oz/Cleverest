using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cleverest.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
      name: "Logout",
      url: "account/logout",
      defaults: new { controller = "Account", action = "Logout" }
    );

            routes.MapRoute(
       name: "Login",
       url: "account/login",
       defaults: new { controller = "Account", action = "Login" }
     );


            routes.MapRoute(
         name: "Registration",
         url: "account/register",
         defaults: new { controller = "Account", action = "Register" }
       );

            routes.MapRoute(
            name: "Rules",
            url: "rules",
            defaults: new { controller = "Home", action = "Rules" }
          );

            routes.MapRoute(
             name: "About",
             url: "about",
             defaults: new { controller = "Home", action = "About" }
           );

            routes.MapRoute(
             name: "Games",
             url: "games",
             defaults: new { controller = "Game", action = "List" }
           );

            routes.MapRoute(
              name: "Home",
              url: "home",
              defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}

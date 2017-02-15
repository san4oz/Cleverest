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
name: "TeamSearch",
url: "account/teams/search",
defaults: new { controller = "Account", action = "Search" }
);

            routes.MapRoute(
name: "MyInvitations",
url: "account/invitations",
defaults: new { controller = "Account", action = "MyInvitations" }
);

            routes.MapRoute(
name: "AccountCreateTeam",
url: "account/team/create",
defaults: new { controller = "Account", action = "CreateTeam" }
);

            routes.MapRoute(
name: "MyTeams",
url: "account/teams",
defaults: new { controller = "Account", action = "MyTeams" }
);

            routes.MapRoute(
name: "MyProfile",
url: "account/profile",
defaults: new { controller = "Account", action = "MyProfile" }
);

            routes.MapRoute(
    name: "GameDetails",
    url: "game/details/{id}",
    defaults: new { controller = "Game", action = "Details" }
  );

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

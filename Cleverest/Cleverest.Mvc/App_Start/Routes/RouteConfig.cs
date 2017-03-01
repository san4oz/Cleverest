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
               name: "StatisticIndex",
               url: "statistic/general",
               defaults: new { controller = "Statistic", action = "Index" }
               );

            routes.MapRoute(
                name: "SetTeamAsSelected",
                url: "game/SetTeamAsSelected",
                defaults: new { controller = "Team", action = "SetTeamAsSelected" }
                );


            routes.MapRoute(
                name: "RegisterTeamOnGame",
                url: "game/registerTeam",
                defaults: new { controller = "Game", action = "RegisterTeam" }
                );
            
                 routes.MapRoute(
           name: "Questions",
           url: "questions/{gameId}/{roundNo}/{orderNo}",
           defaults: new { controller = "Game", action = "Questions" }
           );

            routes.MapRoute(
           name: "editTeamDetails",
           url: "team/edit/{id}",
           defaults: new { controller = "Team", action = "Edit" }
           );

            routes.MapRoute(
            name: "teamDetails",
            url: "team/{id}",
            defaults: new { controller = "Team", action = "Details" }
            );

            routes.MapRoute(
            name: "ProcessRequest",
            url: "account/teams/processrequest",
            defaults: new { controller = "Team", action = "ProcessRequest" }
            );

                        routes.MapRoute(
            name: "SendJoinRequest",
            url: "account/teams/sendjoinrequest",
            defaults: new { controller = "Team", action = "SendJoinRequest" }
            );

                        routes.MapRoute(
            name: "SuggestionDetails",
            url: "account/teams/suggestiondetails",
            defaults: new { controller = "Team", action = "SuggestionDetails" }
            );

                        routes.MapRoute(
            name: "TeamSearch",
            url: "account/teams/search",
            defaults: new { controller = "Team", action = "Search" }
            );

                        routes.MapRoute(
            name: "MyInvitations",
            url: "account/invitations",
            defaults: new { controller = "Team", action = "MyInvitations" }
            );

                        routes.MapRoute(
            name: "AccountCreateTeam",
            url: "account/team/create",
            defaults: new { controller = "Team", action = "CreateTeam" }
            );

                        routes.MapRoute(
            name: "MyTeams",
            url: "account/teams",
            defaults: new { controller = "Team", action = "MyTeams" }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Mvc.Routes;

namespace Cleverest.Mvc.App_Start.Routes
{
    public class AdminAreaRegistration : BaseAreaRegistrator
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        protected override string[] namespaces { get { return new[] { "Cleverest.Mvc.Controllers.Admin" }; } }
        protected override object defaultsComponets { get { return new { controller = "Home", action = "Index", id = UrlParameter.Optional }; } }

        public override void RegisterArea(AreaRegistrationContext registrationContext)
        {
            base.RegisterArea(registrationContext);


            MapRoute(
             name: "RunTask",
             url: "runtask",
             defaults: new { controller = "Task", action = "Run" }
             );

            MapRoute(
             name: "Tasks",
             url: "tasks",
             defaults: new { controller = "Task", action = "Index" }
             );

            MapRoute(
             name: "CreateAccount",
             url: "account/create",
             defaults: new { controller = "Account", action = "Create" }
             );

            MapRoute(
              name: "EditAccount",
              url: "account/edit/{id}",
              defaults: new { controller = "Account", action = "Edit" }
              );

            MapRoute(
                name: "AccountIndex",
                url: "accounts",
                defaults: new { controller = "Account", action = "Index" }
                );

            MapRoute(
              name: "CreateTeam",
              url: "team/create",
              defaults: new { controller = "Team", action = "Create" }
              );

            MapRoute(
              name: "EditTeam",
              url: "team/edit/id",
              defaults: new { controller = "Team", action = "Edit" }
              );

            MapRoute(
                name: "TeamIndex",
                url: "teams",
                defaults: new { controller = "Team", action = "Index" }
                );

            MapRoute(
             name: "DeletePhoto",
             url: "galery/delete",
             defaults: new { controller = "Gallery", action = "Delete" }
             );

            MapRoute(
                name: "GaleryUpload",
                url: "upload",
                defaults: new { controller = "Gallery", action = "Upload" }
                );

            MapRoute(
          name: "GaleryIndex",
          url: "gallery",
          defaults: new { controller = "Gallery", action = "Index" }
      );

            MapRoute(
          name: "EditGame",
          url: "game/edit/{id}",
          defaults: new { controller = "Game", action = "Edit" }
      );

            MapRoute(
           name: "CreateGame",
           url: "game/create",
           defaults: new { controller = "Game", action = "Create" }
       );

            MapRoute(
              name: "GameList",
              url: "games",
              defaults: new { controller = "Game", action = "List" }
          );

            MapRoute(
               name: "Index",
               url: "Index",
               defaults: new { controller = "Home", action = "Index" }
           );

            MapRoute(
                name: "_Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}

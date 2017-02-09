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
                name: "CreateQuestion",
                url: "question/create",
                defaults: new { controller = "Question", action = "Create" }
            );

            MapRoute(
               name: "QuestionList",
               url: "questions",
               defaults: new { controller = "Question", action = "List" }
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

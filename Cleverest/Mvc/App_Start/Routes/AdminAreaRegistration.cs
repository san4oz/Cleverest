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

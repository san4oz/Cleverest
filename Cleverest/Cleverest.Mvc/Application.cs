using Cleverest.App_Start;
using Cleverest.App_Start.Autofac;
using Cleverest.App_Start.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cleverest.Mvc.App_Start;
using System.Web.Optimization;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.Helpers.ImageStorageFactory.Storages;
using Cleverest.Mvc.Security;

namespace Cleverest.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Cleverest.Mvc.Controllers");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            RegisterImageStorages();
        }

        protected void RegisterImageStorages()
        {
            var factory = ImageStorageFactory.Current;

            factory.Register(SiteConstants.ImageStorages.Team, new TeamImageStorage());
            factory.Register(SiteConstants.ImageStorages.Game, new GameImageStorage());
            factory.Register(SiteConstants.ImageStorages.Profile, new ProfileImageStorage());
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            WebSecurity.ProcessPostAuthenticateRequest();
        }
    }
}

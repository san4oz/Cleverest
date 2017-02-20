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

            factory.Register(TeamImageStorage.Name, new TeamImageStorage());
            factory.Register(GameImageStorage.Name, new GameImageStorage());
            factory.Register(ProfileImageStorage.Name, new ProfileImageStorage());
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            Cleverest.Site.Api.Account.ProcessPostAuthenticateRequest();
        }
    }
}

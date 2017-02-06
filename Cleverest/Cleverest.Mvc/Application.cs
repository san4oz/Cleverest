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
            AutoMapperConfig.Configure();
        }
    }
}

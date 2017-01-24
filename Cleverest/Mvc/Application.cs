﻿using Cleverest.App_Start;
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

namespace Cleverest.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Cleverest.Mvc.Controllers");
            //AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.RegisterDependencies();
            AutoMapperConfig.Configure();
        }
    }
}

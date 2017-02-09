using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Cleverest.Mvc;
using Cleverest.Business.InterfaceDefinitions;
using Cleverest.DataProvider;
using Cleverest.Business.Managers;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.DataProvider.Providers;
using Cleverest.Business.InterfaceDefinitions.Providers;
using AutoMapper;
using Cleverest.Mvc.Api;

namespace Cleverest.App_Start.Autofac
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Application).Assembly);

            RegisterTypes(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<MapperConfiguration>().SingleInstance();
            RegisterProviders(builder);
            RegisterManagers(builder);
            RegisterApis(builder);
        }

        private static void RegisterProviders(ContainerBuilder builder)
        {
            builder.RegisterType<GameProvider>().As<IGameProvider>();
            builder.RegisterType<TeamProvider>().As<ITeamProvider>();
            builder.RegisterType<AccountProvider>().As<IAccountProvider>();
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<AccountApi>().SingleInstance();
        }

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<TeamManager>().As<ITeamManager>();
            builder.RegisterType<AccountManager>().As<IAccountManager>();
        }
    }
}

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
using Cleverest.Search;
using Cleverest.Business.InterfaceDefinitions.Search;
using Cleverest.Business.Tasks;

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
            RegisterSearchManagers(builder);
            RegisterTasks(builder);
        }

        private static void RegisterTasks(ContainerBuilder builder)
        {
            builder.RegisterType<TeamIndexTask>().As<ITask>();
        }

        private static void RegisterProviders(ContainerBuilder builder)
        {
            builder.RegisterType<GameProvider>().As<IGameProvider>();
            builder.RegisterType<TeamProvider>().As<ITeamProvider>();
            builder.RegisterType<AccountProvider>().As<IAccountProvider>();
            builder.RegisterType<AccountTeamPermissionProvider>().As<IAccountTeamPermissionProvider>();
        }

        private static void RegisterApis(ContainerBuilder builder)
        {
            builder.RegisterType<AccountApi>().SingleInstance();
            builder.RegisterType<TeamApi>().SingleInstance();
        }

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<TeamManager>().As<ITeamManager>();
            builder.RegisterType<AccountManager>().As<IAccountManager>();
            builder.RegisterType<AccountTeamPermissionManager>().As<IAccountTeamPermissionManager>();
        }

        private static void RegisterSearchManagers(ContainerBuilder builder)
        {
            builder.RegisterType<TeamSearchManager>().As<ITeamSearchManager>();
        }
    }
}

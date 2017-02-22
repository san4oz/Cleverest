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

            RegisterInstances(builder);

            RegisterTypes(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterInstances(ContainerBuilder builder)
        {
            RegisterMapperInstance(builder);
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<MapperConfiguration>().SingleInstance();
            RegisterProviders(builder);
            RegisterManagers(builder);
            RegisterSearchManagers(builder);
            RegisterTasks(builder);
        }

        private static void RegisterMapperInstance(ContainerBuilder builder)
        {
            builder.RegisterInstance<IMapper>(AutoMapper.AutoMapperConfig.GetMapper());
        }

        private static void RegisterTasks(ContainerBuilder builder)
        {
            builder.RegisterType<TeamIndexTask>().As<ITask>();
            builder.RegisterType<ScoreImportTask>().As<ITask>();
        }

        private static void RegisterProviders(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseUnitOfWorkProvider>().As<IDatabaseUnitOfWorkProvider>();
            builder.RegisterType<GameProvider>().As<IGameProvider>();
            builder.RegisterType<TeamProvider>().As<ITeamProvider>();
            builder.RegisterType<AccountProvider>().As<IAccountProvider>();
            builder.RegisterType<AccountTeamPermissionProvider>().As<IAccountTeamPermissionProvider>();
            builder.RegisterType<AccountTeamRequestProvider>().As<IAccountTeamRequestProvider>();
        }

        private static void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<TeamManager>().As<ITeamManager>();
            builder.RegisterType<AccountManager>().As<IAccountManager>();
            builder.RegisterType<AccountTeamPermissionManager>().As<IAccountTeamPermissionManager>();
            builder.RegisterType<AccountTeamRequestManager>().As<IAccountTeamRequestManager>();
        }

        private static void RegisterSearchManagers(ContainerBuilder builder)
        {
            builder.RegisterType<TeamSearchManager>().As<ITeamSearchManager>();
        }
    }
}

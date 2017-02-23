using Cleverest.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.InterfaceDefinitions.Managers;
using AutoMapper;
using Cleverest.Business.InterfaceDefinitions.Providers;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Mvc;

namespace Cleverest
{
    public static class Site
    {
        public static class Managers
        {
            public static IGameManager Game
            {
                get { return Get<IGameManager>(); }
            }

            public static IQuestionManager Question
            {
                get { return Get<IQuestionManager>(); }
            }

            public static ITeamManager Team
            {
                get { return Get<ITeamManager>(); }
            }

            public static IAccountManager Account
            {
                get { return Get<IAccountManager>(); }
            }

            public static IScoreManager Score
            {
                get { return Get<IScoreManager>(); }
            }

            public static IAccountTeamPermissionManager AccountTeamPermission
            {
                get { return Get<IAccountTeamPermissionManager>(); }
            }

            public static IAccountTeamRequestManager AccountTeamRequest
            {
                get { return Get<IAccountTeamRequestManager>(); }
            }
        }

        public static class Providers
        {
            public static IDatabaseUnitOfWorkProvider DatabaseUnitOfWorkProvider
            {
                get { return Get<IDatabaseUnitOfWorkProvider>(); }
            }
        }

        public static class Services
        {
            public static IMapper Mapper
            {
                get { return Get<IMapper>(); }
            }

            public static class ContentStorage
            {
                public static ImageStorage Game
                {
                    get { return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game); }
                }

                public static ImageStorage Team
                {
                    get { return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Team); }
                }

                public static ImageStorage Account
                {
                    get { return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Profile); }
                }
            }
        }

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}

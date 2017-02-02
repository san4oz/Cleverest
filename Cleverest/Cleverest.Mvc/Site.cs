using Cleverest.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.InterfaceDefinitions.Managers;
using AutoMapper;

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
        }

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}

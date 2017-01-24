using Cleverest.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cleverest
{
    public static class Site
    {
        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}

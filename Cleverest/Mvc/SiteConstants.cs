using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc
{
    public static class SiteConstants
    {
        public static class AppSettings
        {
            public static string ImageFolderPath
            {
                get { return ConfigurationManager.AppSettings["ImageFolderPath"]; }
            }
        }

    }
}

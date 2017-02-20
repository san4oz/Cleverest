using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cleverest.Mvc
{
    public static class SiteConstants
    {
        public static class AppSettings
        {
            public static string GameImageFolderPath
            {
                get { return ConfigurationManager.AppSettings["ImageFolderPath"]; }
            }
        }    
        
        public static class Game
        {
            public static class Image
            {
                public const string Logo = "Logo";
            }
        }
        
        public static class Website
        {
            public static class Pathes
            {
                public static string Content = "/Content/";
            }
        }  

        public static class ImageStorages
        {
            public const string Game = "GameStorage";

            public const string Team = "TeamStorage";

            public const string Profile = "ProfileStorage";
        }
    }
}

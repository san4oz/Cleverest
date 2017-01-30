using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Cleverest.Core.Helpers
{
    public class FileHelper
    {
        public static string MapPath(string path)
        {
            return HostingEnvironment.MapPath(path);
        }

        public static string GetFilePath(HttpPostedFileBase file, string pathToSave)
        {
            return HostingEnvironment.MapPath(Path.Combine(pathToSave, file.FileName));
        }

        public static string GetFileRelativePath(HttpPostedFileBase file, string relativePathTosave)
        {
            return string.Format("{0}/{1}", relativePathTosave, file.FileName);
        }
   
        public static void Save(HttpPostedFileBase file, string path)
        {
            file.SaveAs(path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Cleverest.Mvc.Helpers
{
    public static class GameGalleryHelper
    {
        private static DirectoryInfo GetGalleryDirectory(string gameId)
        {
            return new DirectoryInfo(HostingEnvironment.MapPath(GetOrCreateGallery(gameId)));
        }

        private static string GetOrCreateGallery(string gameId)
        {
            var path = Path.Combine(SiteConstants.AppSettings.GameImageFolderPath, gameId);

            var fullPath = HostingEnvironment.MapPath(path);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            return Path.Combine(path);
        }

        public static FileInfo GetLogo(string gameId)
        {
            var folder = GetGalleryDirectory(gameId);

            var logo = folder.GetFiles("Logo").ToList().FirstOrDefault();

            return logo;
        }
        
        public static List<FileInfo> GetGalleryFiles(string gameId)
        {
            return GetGalleryDirectory(gameId)
                        .GetFiles().Where(f => !f.Name.StartsWith(SiteConstants.Game.Image.Logo))
                        .ToList();
        }

        public static List<string> GetGalleryPhotoPathes(string gameId)
        {
            return GetGalleryFiles(gameId).Select(f => GetImageRelativePath(gameId, f.Name)).ToList();
        }

        public static void SaveLogo(string gameId, HttpPostedFileBase logo)
        {
            var pathToSave = HostingEnvironment.MapPath(Path.Combine(GetOrCreateGallery(gameId), string.Format("{0}{1}", SiteConstants.Game.Image.Logo, Path.GetExtension(logo.FileName))));
            logo.SaveAs(pathToSave);
        }

        public static void SavePhoto(string gameId, HttpPostedFileBase photo)
        {
            var pathToSave = HostingEnvironment.MapPath(Path.Combine(GetOrCreateGallery(gameId), photo.FileName));

            photo.SaveAs(pathToSave);
        }

        public static string GetLogoFrontendPath(string gameId)
        {
            var directory = GetGalleryDirectory(gameId);

            var logo = directory.GetFiles(SiteConstants.Game.Image.Logo + "*").FirstOrDefault();

            if (logo == null)
                return string.Empty;

            return string.Format("{0}/{1}", GetOrCreateGallery(gameId), logo.Name);
        }

        private static string GetImageRelativePath(string gameId, string imageName)
        {
            return Path.Combine("/", SiteConstants.AppSettings.GameImageFolderPath, gameId, imageName);
        }
    }
}

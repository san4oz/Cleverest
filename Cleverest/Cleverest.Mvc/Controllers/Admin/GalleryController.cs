using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Cleverest.Core.Helpers;
using Cleverest.Mvc.ViewModels.Admin;
using static Cleverest.Mvc.SiteConstants;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class GalleryController : Controller
    {
        private const int ThumbSize = 160;

        public ActionResult Index(string gameId = null)
        {
            var games = Site.Managers.Game.All();
            var viewModel = new GalleryViewModel();

            var gamesIds = games.Select(game => new { id = game.Id, title = game.Title }).ToList();
            viewModel.GamesIds = gamesIds.Select(game => new SelectListItem() { Text = game.title, Value = game.id }).ToList();

            if (Request.IsAjaxRequest())
            {
                if(!string.IsNullOrEmpty(gameId))
                  viewModel.Photos =  GetPhotos(gameId);

                return PartialView("_Photos", viewModel);
            }

            var defaultGame = games.FirstOrDefault();
            if(defaultGame != null)
            {
                viewModel.Photos = GetPhotos(defaultGame.Id);
            }

            return View(viewModel);
        }

        private List<string> GetPhotos(string gameId)
        {
            var gamePhotosPath = HostingEnvironment.MapPath(Path.Combine(SiteConstants.AppSettings.GameImageFolderPath, gameId));

            if (!Directory.Exists(gamePhotosPath))
                Directory.CreateDirectory(gamePhotosPath);

            var files = new DirectoryInfo(gamePhotosPath).GetFiles().Select(f => GetImageRelativePath(gameId, f.Name)).ToList();

            return files;
        }

        [HttpPost]
        public ActionResult Upload(string gameId = null)
        {
            var files = Request.Files.Cast<string>()
                .Select(f => Request.Files[f])
                .ToArray();

            foreach(var file in files)
            {
                var path = FileHelper.GetFilePath(file, Path.Combine(SiteConstants.AppSettings.GameImageFolderPath, gameId));
                FileHelper.Save(file, path);
            }

            var names = files.Select(file => file.FileName).ToList();

            return Upload(gameId, names);
        }

        [HttpGet]
        public ActionResult Upload(string gameId, List<string> names = null)
        {
            if (string.IsNullOrEmpty(gameId) || names == null)
                return Json(0);

            var path = HostingEnvironment.MapPath(Path.Combine(SiteConstants.AppSettings.GameImageFolderPath, gameId));

            var files = new DirectoryInfo(path).GetFiles()
                .Where(file => names == null || names.Contains(file.Name, StringComparer.OrdinalIgnoreCase))
                .Select(x => new
                    {
                    deleteType = "POST",
                    name = x.Name,
                    size = x.Length,
                    //url, thumbnail, delete
                });

            return Json(new { files }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(List<string> files)
        {
            if (files == null)
                return Json(0);

            foreach(var file in files)
            {
                var pathToDelete = Server.MapPath(file);

                if(System.IO.File.Exists(pathToDelete))
                {
                    System.IO.File.Delete(pathToDelete);
                }
            }

            return Json(files.Count());
        }

        private string GetImageRelativePath(string gameId, string imageName)
        {
            return Path.Combine("/", SiteConstants.AppSettings.GameImageFolderPath, gameId, imageName);
        }
    }
}

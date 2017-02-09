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
using Cleverest.Mvc.Helpers;
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
                if (!string.IsNullOrEmpty(gameId))
                    viewModel.Photos = GameGalleryHelper.GetGalleryPhotoPathes(gameId);

                return PartialView("_Photos", viewModel);
            }

            var defaultGame = games.FirstOrDefault();
            if(defaultGame != null)
            {
                viewModel.Photos = GameGalleryHelper.GetGalleryPhotoPathes(defaultGame.Id);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Upload(string gameId = null)
        {
            var files = Request.Files.Cast<string>()
                .Select(f => Request.Files[f])
                .ToArray();

            foreach(var file in files)
            {
                GameGalleryHelper.SavePhoto(gameId, file);
            }

            return Json(true);
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
    }
}

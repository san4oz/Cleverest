using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels;

namespace Cleverest.Mvc.Controllers
{
    public class GameController : Controller
    {
        public const int PageSize = 10;

        public ActionResult List(int page = 0)
        {
            var games = Site.Managers.Game.All();

            var viewModel = new GamesListViewModel()
            {
                Games = Site.Services.Mapper.Map<IList<Game>, IList<GameViewModel>>(games).OrderBy(m => m.GameDate).ToList(),
                Page = page,
                PageSize = PageSize
            };

            viewModel.Games.ToList().ForEach(game =>
            {
                var imgUrl = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game).GetLogo(game.Id);
                game.ImageUrl = imgUrl == null ? null : imgUrl.ApplicationRelativePath;
            });
                            
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var game = Site.Managers.Game.Get(id);
            if (game == null)
                return HttpNotFound();

            var storage = ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Game);
            var logo = storage.GetLogo(id);

            var viewModel = new GameDetailsViewModel()
            {
                Title = game.Title,
                Location = game.Location,
                GameDate = game.GameDate,
                ImageUrl = logo == null ? null : logo.ApplicationRelativePath
            };

            viewModel.GalleryPhotos = storage.GetImages(id).Select(x => x.ApplicationRelativePath).ToList();
                       
            return View(viewModel);
        }       
    }
}

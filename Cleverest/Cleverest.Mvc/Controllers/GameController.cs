using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Mvc.Helpers;
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
                Games = Mapper.Map<IList<Game>, IList<GameViewModel>>(games).OrderBy(m => m.GameDate).ToList(),
                Page = page,
                PageSize = PageSize
            };

            viewModel.Games.ToList().ForEach(game =>
            {
                game.ImageUrl = GameGalleryHelper.GetLogoFrontendPath(game.Id);
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

            var viewModel = new GameDetailsViewModel()
            {
                Title = game.Title,
                Location = game.Location,
                ImageUrl = GameGalleryHelper.GetLogoFrontendPath(game.Id),
                GameDate = game.GameDate               
            };

            viewModel.GalleryPhotos = GameGalleryHelper.GetGalleryPhotoPathes(id);
                       
            return View(viewModel);
        }       
    }
}

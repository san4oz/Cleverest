using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
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
                Games = Mapper.Map<IList<Game>, IList<GameViewModel>>(games),
                Page = page,
                PageSize = PageSize
            };
                
            return View(viewModel);
        }       
    }
}

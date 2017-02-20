using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions;
using Cleverest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.DataProvider.Providers;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.Helpers.ImageStorageFactory.Storages;

namespace Cleverest.Mvc.Controllers
{
    public class HomeController : Controller
    {      
        [HttpGet]
        public ActionResult Index()
        {
            var team = Site.Managers.Team.All().First();

            var images = ImageStorageFactory.Current.GetStorage(TeamImageStorage.Name).GetLogo(team.Id);

            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rules()
        {
            return View();
        }
    }
}

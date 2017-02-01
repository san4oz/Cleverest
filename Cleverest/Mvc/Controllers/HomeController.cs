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

namespace Cleverest.Mvc.Controllers
{
    public class HomeController : Controller
    {      
        [HttpGet]
        public ActionResult Index()
        {
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

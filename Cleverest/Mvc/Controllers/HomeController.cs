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

namespace Cleverest.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }
    }
}

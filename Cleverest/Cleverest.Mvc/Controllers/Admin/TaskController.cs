using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Tasks;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            var tasks = DependencyResolver.Current.GetServices<ITask>().ToList();

            var viewModels = Mapper.Map<IList<ITask>, IList<TaskViewModel>>(tasks);

            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Run()
        {
            var task = DependencyResolver.Current.GetService<ITask>();
            task.Run();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}

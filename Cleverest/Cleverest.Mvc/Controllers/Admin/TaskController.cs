using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Tasks;
using Cleverest.Mvc.ViewModels.Admin;
using Autofac.Integration.Mvc;
using Autofac;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            var tasks = DependencyResolver.Current.GetServices<ITask>().ToList();

            var viewModels = Site.Services.Mapper.Map<IList<ITask>, IList<TaskViewModel>>(tasks);

            return View(viewModels);
        }

        [HttpPost]
        public ActionResult Run(string taskName)
        {
            var task = AutofacDependencyResolver.Current.ApplicationContainer.ResolveNamed<ITask>(taskName);

            if (task == null)
                throw new KeyNotFoundException("The received key is not present in the collection.");

            task.Run();
            return Json(true);
        }
    }
}

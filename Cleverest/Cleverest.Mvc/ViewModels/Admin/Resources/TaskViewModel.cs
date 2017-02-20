using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Tasks;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class TaskViewModel : IMapFrom<ITask>
    {
        [Display(Name = "Назва")]
        public string Name { get; set; }
    }
}

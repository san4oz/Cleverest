using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class TeamDetailsInfoViewModel : BaseViewModel, IMapFrom<Team>
    {
        [Display(Name = "Ім'я")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Фото")]
        public string PhotoPath { get; set; }

        [Display(Name = "Опис")]
        [Required]
        public string Description { get; set; }

      
    }
}

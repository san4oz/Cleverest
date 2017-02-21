using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;

namespace Cleverest.Mvc.ViewModels.Admin.Resources
{
    public class TeamInputViewModel : BaseInputViewModel, IMapFrom<Team>
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "Про команду")]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        public HttpPostedFileBase Image { get; set; }

        public string OwnerId { get; set; }

        public TeamInputViewModel()
        {
            this.IsMediaContentPresent = true;
        }
    }
}

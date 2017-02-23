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
    public class GameInputViewModel : BaseInputViewModel, IMapFrom<Game>
    {
        [MaxLength(150)]
        [Required]
        [Display(Name = "Назва")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "Дата")]
        public DateTime GameDate { get; set; }

        [MaxLength(250)]
        [Required]
        [Display(Name = "Місце")]
        public string Location { get; set; }

        [Display(Name = "Баннер")]
        public HttpPostedFileBase Image { get; set; }

        public GameInputViewModel()
        {
            IsMediaContentPresent = true;
        }
    }
}

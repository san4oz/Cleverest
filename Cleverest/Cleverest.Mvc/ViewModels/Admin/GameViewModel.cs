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

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class GameViewModel : BaseViewModel, IMapFrom<Game>
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

        [Required]
        [Display(Name = "Баннер")]
        public HttpPostedFileBase Image { get; set; }
    }
}

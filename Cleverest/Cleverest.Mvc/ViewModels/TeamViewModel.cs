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
    public class TeamViewModel : BaseViewModel, IMapFrom<Team>
    {
        [Display(Name = "Назва")]
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Фото")]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Кількість гравців")]
        public int ParticipantsCount { get; set; }
    }
}

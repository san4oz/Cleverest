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
    public class QuestionViewModel : BaseViewModel, IMapFrom<Questions>
    {
        
        [MaxLength(1000)]
        [Required]
        public string Question { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Answer { get; set; }

        [Required]
        public string GameId { get; set; }

        [Required]
        public int RoundNo { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public HttpPostedFileBase Song { get; set; }

    }
}

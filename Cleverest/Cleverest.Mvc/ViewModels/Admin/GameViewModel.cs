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
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime GameDate { get; set; }

        [MaxLength(250)]
        [Required]
        public string Location { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }
    }
}

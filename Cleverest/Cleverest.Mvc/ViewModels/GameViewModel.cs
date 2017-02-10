using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class GameViewModel : BaseViewModel, IMapFrom<Game>
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public DateTime GameDate { get; set; }
    }
}

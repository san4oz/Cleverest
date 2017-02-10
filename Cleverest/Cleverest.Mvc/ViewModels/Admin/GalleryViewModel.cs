using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class GalleryViewModel : BaseViewModel
    {
        [Display(Name = "Гра:")]
        public string SelectedGameId { get; set; }

        public List<SelectListItem> GamesIds { get; set; }

        public List<string> Photos { get; set; }

        public GalleryViewModel()
        {
            this.Photos = new List<string>();
        }
    }
}

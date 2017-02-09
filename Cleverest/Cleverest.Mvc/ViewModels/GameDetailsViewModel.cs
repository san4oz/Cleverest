using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class GameDetailsViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public DateTime GameDate { get; set; }

        public List<string> GalleryPhotos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Frontend.Game
{
    public class GameInfoViewModel
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public DateTime GameDate { get; set; }

        public List<string> GalleryPhotos { get; set; }
    }
}

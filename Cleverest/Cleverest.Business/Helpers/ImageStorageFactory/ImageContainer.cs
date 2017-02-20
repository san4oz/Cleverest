using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Helpers.ImageStorageFactory
{
    public class ImageContainer
    {
        public string ApplicationRelativePath { get; set; }

        public FileInfo Data { get; set; }
    }
}

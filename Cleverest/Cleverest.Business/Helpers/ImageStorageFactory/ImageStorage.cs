using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Helpers.ImageStorageFactory
{
    public abstract class ImageStorage 
    {
        protected abstract string ContentFolderRelativePath { get; }

        private DirectoryInfo RootApplicationDirectory
        {
            get
            {
                var executableFolderPath = AppDomain.CurrentDomain.BaseDirectory;

                return new DirectoryInfo(executableFolderPath);
            }
        }

        private FileInfo EmptyImage
        {
            get
            {
                return new FileInfo(Path.Combine(RootApplicationDirectory.FullName, @"Content\Images\Icons\EmptyImage.png"));
            }
        }

        private DirectoryInfo EntityDirectory
        {
            get
            {
                var targetDirectoryPath = Path.Combine(RootApplicationDirectory.FullName, ContentFolderRelativePath);

                if (!Directory.Exists(targetDirectoryPath))
                    Directory.CreateDirectory(targetDirectoryPath);

                return new DirectoryInfo(targetDirectoryPath);
            }
        }

        private string GetImageRelativePath(string id, string name)
        {
            return string.Format("/{0}/{1}/{2}", ContentFolderRelativePath, id, name);
        }

        protected DirectoryInfo GetInstanceDirectory(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            var targetDirectoryPath = Path.Combine(EntityDirectory.FullName, id);

            if(!Directory.Exists(targetDirectoryPath))
                Directory.CreateDirectory(targetDirectoryPath);            

            return new DirectoryInfo(targetDirectoryPath);
        }

        public virtual ImageContainer GetImage(string entityId, string name)
        {
            var fileInfo = GetInstanceDirectory(entityId).GetFiles(name).SingleOrDefault();
            if (fileInfo == null)
            {
                return new ImageContainer()
                {
                    Data = EmptyImage,
                    ApplicationRelativePath = "/Content/Images/Icons/EmptyImage.png"
                };
            }

            return new ImageContainer()
            {
                Data = fileInfo,
                ApplicationRelativePath = GetImageRelativePath(entityId, fileInfo.Name)
            };
        }

        public virtual void SaveImage(Stream stream, string entityId, string name)
        {
            using (var destinationFile = File.Create(Path.Combine(GetInstanceDirectory(entityId).FullName, name)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(destinationFile);
            }
        }

        public virtual void SaveLogo(Stream stream, string entityId, string path)
        {
            SaveImage(stream, entityId, string.Format("Logo{0}", Path.GetExtension(path)));
        }

        public virtual IList<ImageContainer> GetImages(string entityId)
        {
            var fileInfos = GetInstanceDirectory(entityId).GetFiles();

            return fileInfos.Select(fileInfo => new ImageContainer()
            {
                Data = fileInfo,
                ApplicationRelativePath = GetImageRelativePath(entityId, fileInfo.Name)
            }).ToList();
        }

        public virtual ImageContainer GetLogo(string entityId, string logoName = null)
        {
            return GetImage(entityId, logoName ?? "Logo.*");
        }
    }
}

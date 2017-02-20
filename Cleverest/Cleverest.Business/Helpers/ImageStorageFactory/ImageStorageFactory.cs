using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Helpers.ImageStorageFactory
{
    public class ImageStorageFactory
    {
        private static volatile ImageStorageFactory Instance;
        private static object syncObj = new object();

        public static ImageStorageFactory Current
        {
            get
            {
                if(Instance == null)
                {
                    lock(syncObj)
                    {
                        Instance = new ImageStorageFactory();
                    }
                }

                return Instance;
            }
        }

        protected Dictionary<string, ImageStorage> factory;

        private ImageStorageFactory()
        {
            factory = new Dictionary<string, ImageStorage>();
        }

        public virtual void Register(string type, ImageStorage storage)
        {
            factory[type] = storage;
        }

        public virtual void Unregister(string type)
        {
            factory.Remove(type);
        }

        public ImageStorage GetStorage(string type)
        {
            ImageStorage storage = null;
            if (factory.TryGetValue(type, out storage))
                return storage;
            else
                throw new KeyNotFoundException(string.Format("Storage for type '{0}' cannot be found", type));
        }
    }
}

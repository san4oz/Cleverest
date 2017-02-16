using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.App_Start.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            return new MapperConfiguration(x => 
            {
                Configure(x);
            })
            .CreateMapper();
        }

        public static void Configure(IMapperConfiguration mapperConfiguration)
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            LoadStandartMappings(mapperConfiguration, types);

            LoadCustomMappings(mapperConfiguration, types);
        }

        #region private

        private static void LoadStandartMappings(IMapperConfiguration mapperConfiguration, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                        select new
                        {
                            Source  = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray(); 

            foreach(var map in maps)
            {                
                mapperConfiguration.CreateMap(map.Source, map.Destination);
                mapperConfiguration.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IMapperConfiguration mapperConfiguration, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces() 
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach(var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Search.Persisters
{
    public class IndexFieldPersisterBase
    {
        protected virtual object GetPropertyValue(Entity entity, string propertyName)
        {
            var propertyInfo = entity.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
                return propertyInfo.GetValue(entity, null);

            return null;
        }

        protected virtual string GetStringValue(object value)
        {
            if (value == null)
                return string.Empty;

            return value.ToString();
        }

        public virtual string ExtractKeywords(Entity entity, string fieldName)
        {
            return GetStringValue(GetPropertyValue(entity, fieldName));
        }

        public virtual object ParseRawValue(string rawValue)
        {
            return rawValue;
        }
    }
}

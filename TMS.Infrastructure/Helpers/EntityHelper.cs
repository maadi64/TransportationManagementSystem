using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Infrastructure.Helpers
{
    public static class EntityHelper<T> where T : class
    {
        public static DynamicParameters GetDynamicParameters(T entity)
        {
            Type entityType = entity.GetType();
            var props = entityType.GetProperties();
            var parameters = new DynamicParameters();
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType.Name.ToLower() != "string")
                {
                    continue;
                }

                parameters.Add(prop.Name, prop.GetValue(entity), DbHelper.GetDbType(prop.PropertyType));
            }
            return parameters;
        }
    }
}

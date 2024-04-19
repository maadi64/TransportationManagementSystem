using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Infrastructure.Helpers
{
    public static class DynamicQueryHelper
    {
        public static string GetSelectQuery<T>() => $"SELECT * FROM {typeof(T).Name}";
        //public static string GetPaginatedQuery<T>(int pageNo, int PageSize)=> $"SELECT *FROM {typeof(T).Name} ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY" ;
        public static string GetSelectByIdQuery<T>(string idField) => $"SELECT * FROM {typeof(T).Name} WHERE {idField} = @{idField}";

        public static string GetInsertQuery<T>()
        {
            List<PropertyInfo> propertyInfos = typeof(T).GetProperties().ToList();
            propertyInfos.Remove(propertyInfos?.FirstOrDefault(x => x.Name == "Id"));

            StringBuilder properties = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsClass
                    && propertyInfo.PropertyType.Name.ToLower() != "string")
                {
                    continue;
                }
                if (propertyInfos.First().Name == propertyInfo.Name)
                {
                    properties.Append(propertyInfo.Name);
                    values.Append($"@{propertyInfo.Name}");
                }
                else
                {
                    properties.Append($", {propertyInfo.Name}");
                    values.Append($", @{propertyInfo.Name}");
                }
            }

            return $"INSERT INTO {typeof(T).Name} ({properties}) VALUES ({values}); SELECT SCOPE_IDENTITY() AS Id;";
        }

        public static string GetUpdateQuery<T>(string idField)
        {
            List<PropertyInfo> propertyInfos = typeof(T).GetProperties().ToList();
            propertyInfos.Remove(propertyInfos.FirstOrDefault(x => x.Name == idField));

            StringBuilder values = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsClass
                    && propertyInfo.PropertyType.Name.ToLower() != "string")
                {
                    continue;
                }
                if (propertyInfos.First().Name == propertyInfo.Name)
                {
                    values.Append($"{propertyInfo.Name} = @{propertyInfo.Name}");
                }
                else
                {
                    values.Append($", {propertyInfo.Name} = @{propertyInfo.Name}");
                }
            }

            return $"UPDATE {typeof(T).Name} SET {values} WHERE {idField} = @{idField}";
        }

        public static string GetDeleteQuery<T>(string fieldName) =>
            $"DELETE FROM {typeof(T).Name} WHERE {fieldName} = @Value";
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Infrastructure.Helpers
{
    public static class DbHelper
    {
        public static DbType GetDbType(Type propertyType)
        {
            switch (propertyType.Name)
            {
                case "Int32":
                    return DbType.Int32;
                case "String":
                    return DbType.String;
                case "Double":
                    return DbType.Double;
                case "DateTime":
                    return DbType.DateTime;
            }
            return DbType.String;
        }
    }
}

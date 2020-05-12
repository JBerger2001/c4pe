using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Feedback_API.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                var typeCode = Type.GetTypeCode(objectProperty.PropertyType);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.String:
                    case TypeCode.Char:
                    case TypeCode.DateTime:
                        break;

                    default:
                        continue;
                }

                if (objectProperty == null)
                {
                    continue;
                }

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrEmpty(orderQuery))
            {
                return entities;
            }

            return entities.OrderBy(orderQuery);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> entities, string orderByQuery)
        {
            return entities.AsQueryable().Sort(orderByQuery);
        }
    }
}

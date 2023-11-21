using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class FromParams
    {
        public static IEnumerable<T> Enumerable<T>(params T[] items)
        {
            return items;
        }
        
        public static List<T> List<T>(params T[] items)
        {
            return items.ToList();
        }
    }
}
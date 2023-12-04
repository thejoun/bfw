using System.Collections.Generic;
using System.Linq;

namespace Utilities
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
        
        public static T[] Array<T>(params T[] items)
        {
            return items;
        }
        
        public static object[] Array(params object[] items)
        {
            return items;
        }
    }
}
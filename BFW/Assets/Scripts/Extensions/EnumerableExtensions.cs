using System.Collections.Generic;

namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> FromParams<T>(params T[] elements)
        {
            return elements;
        }
    }
}
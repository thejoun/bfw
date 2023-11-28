using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    public interface IKeyed<out T>
    {
        T Key { get; }
    }

    public static class IKeyedExtensions
    {
        public static T FirstWithKey<T, TKey>(this IEnumerable<T> values, TKey key) 
            where T : IKeyed<TKey>
        {
            return values.FirstOrDefault(value => key.Equals(value.Key));
        }
        
        public static bool TryGetFirstWithKey<T, TKey>(this IEnumerable<T> values, TKey key, out T value) 
            where T : IKeyed<TKey>
        {
            value = values.FirstOrDefault(value => key.Equals(value.Key));
            
            return value != null;
        }
    }
}
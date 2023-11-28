using System.Collections.Generic;
using Interfaces;

namespace Core
{
    public class Registry<T> : IRegistry<T>
    {
        private readonly List<T> list = new();

        public IEnumerable<T> Values => list;
        
        public void Add(T value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }

        public void Remove(T value)
        {
            list.Remove(value);
        }
    }
}
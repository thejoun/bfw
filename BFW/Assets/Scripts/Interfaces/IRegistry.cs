using System.Collections.Generic;

namespace Interfaces
{
    public interface IRegistry<T>
    {
        IEnumerable<T> Values { get; }

        void Add(T value);
        void Remove(T value);
    }
}
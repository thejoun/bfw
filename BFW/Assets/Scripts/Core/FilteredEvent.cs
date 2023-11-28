using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Core
{
    public class FilteredEvent<TValue, TFilter, TInput> : IFilteredEvent<TValue, TFilter, TInput>
        where TFilter : IFilter<TInput>
    {
        private readonly List<(object listener, TFilter filter, Action<TValue> action)> entries = new();

        public void Raise(TValue value, TInput input)
        {
            foreach (var entry in entries.ToList())
            {
                if (entry.filter == null || entry.filter.Accepts(input))
                {
                    entry.action?.Invoke(value);
                }
            }
        }

        public void Register(object listener, TFilter filter, Action<TValue> action)
        {
            entries.Add((listener, filter, action));
        }

        public void Unregister(object listener)
        {
            entries.RemoveAll(entry => entry.listener == listener);
        }
    }
}
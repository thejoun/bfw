using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Serializables.General
{
    public class Event : IEvent
    {
        private readonly List<(object listener, Action action)> entries = new();

        public void Raise()
        {
            foreach (var entry in entries.ToList())
            {
                entry.action?.Invoke();
            }
        }

        public void Register(object listener, Action action)
        {
            entries.Add((listener, action));
        }

        public void Unregister(object listener)
        {
            entries.RemoveAll(entry => entry.listener == listener);
        }
    }

    public class Event<T> : IEvent<T>
    {
        private readonly List<(object listener, Action<T> action)> entries = new();

        public void Raise(T value)
        {
            foreach (var entry in entries.ToList())
            {
                entry.action?.Invoke(value);
            }
        }

        public void Register(object listener, Action<T> action)
        {
            entries.Add((listener, action));
        }

        public void Unregister(object listener)
        {
            entries.RemoveAll(entry => entry.listener == listener);
        }
    }
    
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
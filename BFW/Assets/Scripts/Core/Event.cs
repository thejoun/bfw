using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Core
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
}
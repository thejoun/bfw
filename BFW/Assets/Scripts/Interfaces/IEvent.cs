using System;

namespace Interfaces
{
    public interface IEvent
    {
        void Register(object listener, Action action);
        void Unregister(object listener);

        void Raise();
    }
    
    public interface IEvent<T>
    {
        void Register(object listener, Action<T> action);
        void Unregister(object listener);

        void Raise(T value);
    }
}
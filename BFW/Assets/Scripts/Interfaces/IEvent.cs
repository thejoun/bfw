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
    
    public interface IFilteredEvent<TValue, in TFilter, in TInput> 
        : IFilteredRaisable<TValue, TInput>, IFilteredListenable<TValue, TFilter>
    {
        
    }
    
    public interface IFilteredRaisable<in TValue, in TInput>
    {
        void Raise(TValue value, TInput input);
    }
    
    public interface IFilteredListenable<out TValue, in TFilter>
    {
        void Register(object listener, TFilter filter, Action<TValue> action);
        void Unregister(object listener);
    }
}
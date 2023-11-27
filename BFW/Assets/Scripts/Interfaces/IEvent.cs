using System;

namespace Interfaces
{
    public interface IEvent : IRaisable, IListenable
    {
        
    }

    public interface IRaisable
    {
        void Raise();
    }

    public interface IListenable
    {
        void Register(object listener, Action action);
        void Unregister(object listener);
    }
    
    public interface IEvent<T> : IRaisable<T>, IListenable<T>
    {
        
    }

    public interface IRaisable<in T>
    {
        void Raise(T value);
    }

    public interface IListenable<out T>
    {
        void Register(object listener, Action<T> action);
        void Unregister(object listener);
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
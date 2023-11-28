using Const;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using Zenject;

namespace ECS.Core
{
    public abstract class ValueComponent<T> : EntityComponent
    {
        [Inject(Id = ID.ComponentValueSetFilteredEvent)]
        private IFilteredListenable<byte[], EntityAddressFilter> valueSetEvent;

        [ShowInInspector] [HideInEditorMode] 
        public T Value { get; protected set; }

        private EntityAddressFilter Filter => new(Entity.Id, Contract);
        
        protected virtual void OnEnable()
        {
            valueSetEvent.Register(this, Filter, OnValueChanged);
        }

        protected virtual void OnDisable()
        {
            valueSetEvent.Unregister(this);
        }

        public ValueComponent<T> WithValue(T value)
        {
            SetValue(value);
            return this;
        }
        
        public virtual void SetValue(T value)
        {
            Value = value;
            OnValueChanged(value);
        }

        protected abstract void OnValueChanged(byte[] bytes);
        protected abstract void OnValueChanged(T value);
    }
}
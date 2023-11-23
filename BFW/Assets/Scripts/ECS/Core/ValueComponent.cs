using Core;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using Zenject;

namespace ECS.Core
{
    public abstract class ValueComponent<T> : EntityComponent
    {
        [Inject(Id = EventID.EntityComponentValueSet)]
        private IFilteredListenable<byte[], EntityAddressFilter> valueSetEvent;

        [ShowInInspector] [HideInEditorMode] public T Value { get; private set; }

        protected virtual EntityAddressFilter Filter => new(Entity.Id, Contract);
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            valueSetEvent.Register(this, Filter, OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            valueSetEvent.Unregister(this);
        }

        public virtual void SetValue(T value)
        {
            Value = value;
            
            OnValueChanged(value);
        }

        protected virtual void OnValueChanged(byte[] bytes)
        {
            // to implement optionally
        }
        
        protected virtual void OnValueChanged(T value)
        {
            // to implement optionally
        }
    }
}
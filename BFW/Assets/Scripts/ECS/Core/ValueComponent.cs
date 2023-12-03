using System;
using System.Collections.Generic;
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

        private static readonly Dictionary<IEntity, T> values = new();

        [ShowInInspector] [HideInEditorMode] 
        public T Value { get; protected set; }

        private EntityAddressFilter Filter => new(Entity.Id, Contract);

        public static IDictionary<IEntity, T> Values => values;

        public static event Action<IEntity, T> ValueChanged;
        
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

        protected virtual void OnValueChanged(T value)
        {
            if (values.ContainsKey(Entity))
            {
                values[Entity] = value;
            }
            else
            {
                values.Add(Entity, value);
            }
            
            ValueChanged?.Invoke(Entity, value);
        }
        
        protected abstract void OnValueChanged(byte[] bytes);
    }
}
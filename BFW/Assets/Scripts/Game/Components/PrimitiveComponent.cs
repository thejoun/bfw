using UnityEngine;

namespace Game.Components
{
    public abstract class PrimitiveComponent<T> : EntityComponent
    {
        [field: SerializeField] public T Value { get; private set; }
        
        public virtual void SetValue(T value)
        {
            Value = value;
        }
    }
}
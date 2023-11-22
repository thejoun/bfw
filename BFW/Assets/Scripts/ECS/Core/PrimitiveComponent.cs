using ECS.Components;
using Sirenix.OdinInspector;

namespace ECS.Core
{
    public abstract class PrimitiveComponent<T> : EntityComponent
    {
        [ShowInInspector] [HideInEditorMode] 
        public T Value { get; private set; }
        
        public virtual void SetValue(T value)
        {
            Value = value;
            
            OnValueChanged(value);
        }
        
        protected virtual void OnValueChanged(T value)
        {
            // to implement optionally
        }
    }
}
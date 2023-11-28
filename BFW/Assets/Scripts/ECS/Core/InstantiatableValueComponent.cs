namespace ECS.Core
{
    public abstract class InstantiatableValueComponent<T> : ValueComponent<T>
    {
        private bool isInstantiated;
        
        protected virtual void Start()
        {
            Instantiate();
            
            isInstantiated = true;
            
            OnValueChanged(Value);
        }

        public override void SetValue(T value)
        {
            Value = value;

            if (isInstantiated)
            {
                OnValueChanged(value);
            }
        }

        protected abstract void Instantiate();
    }
}
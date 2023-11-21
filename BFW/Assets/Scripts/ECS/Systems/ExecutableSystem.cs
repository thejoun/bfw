using Sirenix.OdinInspector;

namespace ECS.Systems
{
    public abstract class ExecutableSystem<T> : EntitySystem
    {
        [Button]
        [HideInEditorMode]
        public virtual void Execute(T arguments)
        {
            ExecuteLocal(arguments);
            ExecuteRemote(arguments);
        }
        
        protected virtual void ExecuteLocal(T arguments)
        {
            // to implement
        }

        protected virtual void ExecuteRemote(T arguments)
        {
            // to implement
        }
    }
}
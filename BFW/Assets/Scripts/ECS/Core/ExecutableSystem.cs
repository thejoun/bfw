using Sirenix.OdinInspector;

namespace ECS.Systems
{
    public abstract class ExecutableSystem<T> : EcsSystem
    {
        [Button]
        [HideInEditorMode]
        public virtual void Execute(int entityId, T arguments)
        {
            ExecuteLocal(entityId, arguments);
            ExecuteRemote(entityId, arguments);
        }

        protected abstract void ExecuteLocal(int entityId, T arguments);
        protected abstract void ExecuteRemote(int entityId, T arguments);
    }
}
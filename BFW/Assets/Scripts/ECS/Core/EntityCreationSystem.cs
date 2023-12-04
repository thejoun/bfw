using Const;
using Interfaces;
using Sirenix.OdinInspector;
using Zenject;

namespace ECS.Systems
{
    public abstract class EntityCreationSystem<T> : ExecutableEntitySystem<T>
    {
        [Inject(Id = ID.EntityRegistry)] private IEntityRegistry entityRegistry;

        [Button]
        [HideInEditorMode]
        public virtual void Execute(T arguments)
        {
            var entityId = entityRegistry.GetNewId();
            
            ExecuteLocal(entityId, arguments);
            ExecuteRemote(entityId, arguments);
        }

        protected override void ExecuteLocal(int entityId, T arguments)
        {
            // do nothing
        }
    }
}
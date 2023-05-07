using Sirenix.OdinInspector;

namespace Game.Systems
{
    public abstract class ExecutableSystem<TIn> : EntitySystem
    {
        [Button] [HideInEditorMode]
        public abstract void Execute(TIn arguments);
    }
}
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;

namespace ECS.Systems
{
    public abstract class ExecutableEntitySystem<T> : EcsSystem
    {
        [Button]
        [HideInEditorMode]
        public virtual void Execute(int entityId, T arguments)
        {
            ExecuteLocal(entityId, arguments);
            ExecuteRemote(entityId, arguments);
        }

        protected abstract object[] GetRemoteArguments(T value);
        
        protected abstract void ExecuteLocal(int entityId, T arguments);

        protected virtual void ExecuteRemote(int entityId, T arguments)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");

            var remoteArguments = GetRemoteArguments(arguments);
            var paramsArguments = new object[] { entityId }.Union(remoteArguments).ToArray();
            
            function.ExecuteAsync(account, gasLimit, paramsArguments)
                .WithCallback(OnReceiptReceived);
        }
    }
}
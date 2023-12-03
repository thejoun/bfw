using Extensions;
using Sirenix.OdinInspector;

namespace ECS.Systems
{
    public class ArchetypeDesignSystem : EcsSystem
    {
        [Button] [HideInEditorMode]
        public void Execute(int entityId, int movementPoints)
        {
            ExecuteLocal(entityId, movementPoints);
            ExecuteRemote(entityId, movementPoints);
        }

        private void ExecuteLocal(int entityId, int movementPoints)
        {
            // Nothing. Wait for confirmation
        }

        private void ExecuteRemote(int entityId, int movementPoints)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entityId, movementPoints)
                .WithCallback(OnReceiptReceived);
        }
    }
}
using Extensions;
using Sirenix.OdinInspector;

namespace ECS.Systems
{
    public class TerrainDesignSystem : EcsSystem
    {
        [Button] [HideInEditorMode]
        public void Execute(int entityId, int movementCost)
        {
            ExecuteLocal(entityId, movementCost);
            ExecuteRemote(entityId, movementCost);
        }

        private void ExecuteLocal(int entityId, int movementCost)
        {
            // Nothing. Wait for confirmation
        }

        private void ExecuteRemote(int entityId, int movementCost)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entityId, movementCost)
                .WithCallback(OnReceiptReceived);
        }
    }
}
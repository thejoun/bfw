using Interfaces;
using Sirenix.OdinInspector;
using Unimorph.Fields;

namespace Contracts.Controllers
{
    public class AccessController : ContractController
    {
        private Reference<IContract> whoHas;
        private Reference<IContract> toWhere;
        
        [Button]
        private async void Check()
        {
            await Web.Ecs.HasAccess(whoHas.Value, toWhere.Value);
        }
    }
}
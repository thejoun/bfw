using System.Threading.Tasks;
using Interfaces;

namespace Objects
{
    public class Ecs : IEcs
    {
        private const string HasWriteAccessFunction = "writeAccess";

        private IEcsWeb Web { get; set; }

        protected Ecs(IEcsWeb web)
        {
            Web = web;
        }

        public virtual async Task<bool> HasAccess(IAddress who, IContract where)
        {
            var contract = Web.GetContract(where);
            var function = contract.GetFunction(HasWriteAccessFunction);
            
            var result = await function.CallAsync<bool>(who.AddressHex);
            
            return result;
        }
    }
}
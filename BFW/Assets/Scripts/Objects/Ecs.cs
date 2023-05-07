using System.Threading.Tasks;
using Interfaces;

namespace Objects
{
    public class Ecs : IEcs
    {
        public const string HasWriteAccessFunction = "writeAccess";

        public IEcsWeb Web { get; private set; }

        public Ecs(IEcsWeb web)
        {
            Web = web;
        }

        public virtual async Task<bool> HasAccess(IAddress who, IContract where)
        {
            var contract = Web.GetContract(where);
            var function = contract.GetFunction(HasWriteAccessFunction);
            
            var result = await function.CallAsync<bool>(who.Address);
            
            return result;
        }
    }
}
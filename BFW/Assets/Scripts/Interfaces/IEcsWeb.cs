using Nethereum.Contracts;
using Nethereum.Web3;

namespace Interfaces
{
    public interface IEcsWeb : IWeb3
    {
        IEcs Ecs { get; }
        
        Contract GetContract(IContract contract);
    }
}
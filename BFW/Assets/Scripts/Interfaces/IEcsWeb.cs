using Nethereum.Contracts;
using Nethereum.Web3;

namespace Interfaces
{
    public interface IEcsWeb : IWeb3
    {
        IEcs Ecs { get; }
        INode Node { get; }
        
        Contract GetContract(IContract contract);
    }
}
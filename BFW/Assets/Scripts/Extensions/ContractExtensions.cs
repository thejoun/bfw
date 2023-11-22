using Interfaces;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace Extensions
{
    public static class ContractExtensions
    {
        public static Contract GetInstance(this IContract contract, IWeb3 web)
        {
            return web.Eth.GetContract(contract.Abi, contract.AddressHex);
        }
    }
}
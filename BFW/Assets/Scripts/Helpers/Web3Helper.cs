using Interfaces;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Helpers
{
    public static class Web3Helper
    {
        public static Web3 CreateWeb3(INode node, IAccount account)
        {
            var web3Account = new Account(account.PrivateKey);
            var web3 = new Web3(web3Account, node.Url);

            return web3;
        }
    }
}
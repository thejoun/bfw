using Interfaces;
using Nethereum.BlockchainProcessing.Services;
using Nethereum.Contracts;
using Nethereum.Contracts.Services;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC;
using Nethereum.RPC.DebugNode;
using Nethereum.RPC.TransactionManagers;
using Nethereum.RPC.TransactionReceipts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Objects
{
    public class EcsWeb : IEcsWeb
    {
        private Web3 Web3 { get; set; }
        
        public IAccount Account { get; private set; }
        public INode Node { get; private set; }
        public IEcs Ecs { get; private set; }

        public IClient Client => Web3.Client;
        public IEthApiContractService Eth => Web3.Eth;
        public IBlockchainProcessingService Processing => Web3.Processing;
        public INetApiService Net => Web3.Net;
        public IPersonalApiService Personal => Web3.Personal;
        public IShhApiService Shh => Web3.Shh;
        public IDebugApiService Debug => Web3.Debug;
        public FeeSuggestionService FeeSuggestion => Web3.FeeSuggestion;
        
        public ITransactionManager TransactionManager
        {
            get => Eth.TransactionManager;
            set => Eth.TransactionManager = value;
        }
        
        public ITransactionReceiptService TransactionReceiptPolling
        {
            get => TransactionManager?.TransactionReceiptService;
            set => TransactionManager.TransactionReceiptService = value;
        }
        
        public EcsWeb(IAccount account, INode node)
        {
            var web3Account = new Account(account.PrivateKey);
            
            Account = account;
            Node = node;
            Web3 = new Web3(web3Account, node.Url);
            Ecs = new LogEcs(this);
        }

        public Contract GetContract(IContract contract)
        {
            // todo maybe cache the contracts unless data changes
            
            var instance = Eth.GetContract(contract.Abi, contract.AddressHex);

            return instance;
        }
    }
}
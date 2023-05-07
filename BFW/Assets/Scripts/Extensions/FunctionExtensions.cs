using System.Threading.Tasks;
using Interfaces;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Extensions
{
    public static class FunctionExtensions
    {
        public static async Task<TransactionReceipt> ExecuteAsync(this Function function, 
            IAccount account, int gas, params object[] arguments)
        {
            var gasHex = new HexBigInteger(gas);
            
            return await function.SendTransactionAndWaitForReceiptAsync(account.Address, gasHex, 
                null, null, arguments);
        }
        
        public static async Task<TransactionReceipt> ExecuteAsync(this Function function, 
            IAccount account, HexBigInteger gas, params object[] arguments)
        {
            return await function.SendTransactionAndWaitForReceiptAsync(account.Address, gas, 
                null, null, arguments);
        }
    }
}
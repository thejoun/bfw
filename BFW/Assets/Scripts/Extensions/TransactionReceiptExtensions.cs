using Nethereum.RPC.Eth.DTOs;

namespace Extensions
{
    public static class TransactionReceiptExtensions
    {
        public static bool Success(this TransactionReceipt receipt)
        {
            return receipt.Status.ToString() == "1";
        }
    }
}
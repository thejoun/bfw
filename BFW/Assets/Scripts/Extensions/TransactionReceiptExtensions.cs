using Nethereum.RPC.Eth.DTOs;

namespace Extensions
{
    public static class TransactionReceiptExtensions
    {
        public static bool IsSuccess(this TransactionReceipt receipt)
        {
            return receipt.Status.ToString() == "1";
        }
    }
}
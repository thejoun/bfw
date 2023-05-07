using System.Text;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine;

namespace Game.Systems
{
    public abstract class ExecutableSystem<TIn> : EcsSystem
    {
        public abstract void Execute(TIn arguments);
        
        protected virtual void OnReceiptReceived(TransactionReceipt receipt)
        {
            var status = receipt.Status.ToString();
            
            var sb = new StringBuilder();
            sb.Append($"{nameof(MovementSystem)}: ");

            if (status == "1")
            {
                Success?.Invoke();
                sb.Append("success");
            }
            else
            {
                Failed.Invoke();
                sb.Append("failed");
            }

            sb.Append($"\nGas used: {receipt.GasUsed}");
            Debug.Log(sb.ToString());
        }
    }
}
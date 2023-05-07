using System;
using System.Text;
using Extensions;
using Interfaces;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public abstract class EcsSystem : MonoBehaviour
    {
        [Inject] protected IEcsWeb web;
        [Inject] protected IAccount account;
        
        [Inject(Id = "GasLimit")] protected HexBigInteger gasLimit;
        
        public Action Success;
        public Action Failed;
        
        protected virtual void OnReceiptReceived(TransactionReceipt receipt)
        {
            var success = receipt.Success();
            
            var sb = new StringBuilder();
            sb.Append($"{GetType().Name}: ");
            sb.Append(success ? "success" : "failed");
            sb.Append($"\nGas used: {receipt.GasUsed}");
            Debug.Log(sb.ToString());

            if (success) OnSuccess();
            else OnFailed();
        }

        protected virtual void OnSuccess()
        {
            Success?.Invoke();
        }

        protected virtual void OnFailed()
        {
            Failed?.Invoke();
        }
    }
}
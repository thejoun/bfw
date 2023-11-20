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
        [Inject] protected IContract contract;
        
        [Inject(Id = "GasLimit")] protected HexBigInteger gasLimit;
        
        public Action Success;
        public Action Failed;
        
        protected void Execute(params object[] input)
        {
            // to implement
        }
        
        protected virtual void OnReceiptReceived(TransactionReceipt receipt)
        {
            var isSuccess = receipt.IsSuccess();
            
            LogReceipt(receipt, isSuccess);

            OnResult(isSuccess);
        }

        private void OnResult(bool isSuccess)
        {
            if (isSuccess) OnSuccess();
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

        private void LogReceipt(TransactionReceipt receipt, bool isSuccess)
        {
            var typeName = GetType().Name;
            var status = isSuccess ? "success" : "failed";
            var gas = receipt.GasUsed;

            var log = $"{typeName}: {status}\nGas used: {gas}";

            Debug.Log(log);
        }
    }
}
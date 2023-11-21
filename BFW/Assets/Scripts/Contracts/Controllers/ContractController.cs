using System;
using Context;
using Interfaces;
using Nethereum.RPC.Eth.DTOs;
using Objects;
using Unimorph.Core;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable] [ManualInject]
    public class ContractController
    {
        protected IAccount Account => EditorContext.Instance.Account;
        protected EcsWeb Web => EditorContext.Instance.Web;
        
        protected int GasLimit => 10000000;

        protected void LogReceipt(TransactionReceipt receipt, string log)
        {
            Debug.Log($"{log}: status {receipt.Status}; gas used {receipt.GasUsed}.");
        }
    }
}
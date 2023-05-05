using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class ContractAssetReference : IContract
    {
        [AssetSelector] [HideLabel]
        [SerializeField] private ContractAsset account;

        public string Address => account.Address ?? string.Empty;
        public string Abi => account.Abi ?? string.Empty;
    }
}
using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class AccountAssetReference : IAccount
    {
        [AssetSelector] [HideLabel]
        [SerializeField] private AccountAsset account;

        public string Address => account.Address ?? string.Empty;
        public string PrivateKey => account.PrivateKey ?? string.Empty;
    }
}
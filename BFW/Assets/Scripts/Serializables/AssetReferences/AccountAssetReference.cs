using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class AccountAssetReference : AssetReference<AccountAsset>, IAccount
    {
        public string Address => Asset.Address ?? string.Empty;
        public string PrivateKey => Asset.PrivateKey ?? string.Empty;
    }
}
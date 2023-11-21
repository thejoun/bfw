using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class AddressAssetReference : AssetReference<AddressAsset>, IAddress
    {
        public string AddressHex => Asset.AddressHex ?? string.Empty;
    }
}
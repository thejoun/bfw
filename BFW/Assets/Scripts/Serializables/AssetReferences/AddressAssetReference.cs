using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class AddressAssetReference : IAddress
    {
        [AssetSelector] [HideLabel]
        [SerializeField] private AddressAsset address;

        public string Address => address.Address ?? string.Empty;
    }
}
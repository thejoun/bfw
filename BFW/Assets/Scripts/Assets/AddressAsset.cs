using Const;
using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AddressAsset), 
        menuName = MenuName.Assets + nameof(AddressAsset))]
    public class AddressAsset : FieldAsset<Address>, IAddress
    {
        public string AddressHex => Field.AddressHex ?? string.Empty;
    }
}
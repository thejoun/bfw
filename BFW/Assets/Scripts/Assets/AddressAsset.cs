using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AddressAsset), 
        menuName = MenuName.Assets + nameof(AddressAsset))]
    public class AddressAsset : FieldAsset<AddressField>, IAddress
    {
        public string Address => Field.Address ?? string.Empty;
    }
}
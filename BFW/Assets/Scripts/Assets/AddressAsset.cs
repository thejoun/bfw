using Core;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AddressAsset), 
        menuName = MenuName.Assets + nameof(AddressAsset))]
    public class AddressAsset : ScriptableObject, IAddress
    {
        [LabelWidth(ShortLabel.Width)] [HideLabel]
        [SerializeField] private AddressField address;

        public string Address => address.Address ?? string.Empty;
    }
}
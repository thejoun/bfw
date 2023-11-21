using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(ContractAsset), 
        menuName = MenuName.Assets + nameof(ContractAsset))]
    public class ContractAsset : FieldAsset<Contract>, IContract
    {
        public string Title => Field.Title ?? string.Empty;
        public string AddressHex => Field.AddressHex ?? string.Empty;
        public string Abi => Field.Abi ?? string.Empty;
    }
}
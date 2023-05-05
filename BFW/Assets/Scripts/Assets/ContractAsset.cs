using Core;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(ContractAsset), 
        menuName = MenuName.Assets + nameof(ContractAsset))]
    public class ContractAsset : ScriptableObject, IContract
    {
        [LabelWidth(ShortLabel.Width)] [HideLabel]
        [SerializeField] private ContractField account;

        public string Address => account.Address ?? string.Empty;
        public string Abi => account.Abi ?? string.Empty;
    }
}
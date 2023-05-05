using Core;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AccountAsset), 
        menuName = MenuName.Assets + nameof(AccountAsset))]
    public class AccountAsset : ScriptableObject, IAccount
    {
        [LabelWidth(ShortLabel.Width)] [HideLabel]
        [SerializeField] private AccountField account;

        public string Address => account.Address ?? string.Empty;
        public string PrivateKey => account.PrivateKey ?? string.Empty;
    }
}
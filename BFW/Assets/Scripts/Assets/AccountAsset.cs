using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AccountAsset), 
        menuName = MenuName.Assets + nameof(AccountAsset))]
    public class AccountAsset : FieldAsset<AccountField>, IAccount
    {
        public string Address => Field.Address ?? string.Empty;
        public string PrivateKey => Field.PrivateKey ?? string.Empty;
    }
}
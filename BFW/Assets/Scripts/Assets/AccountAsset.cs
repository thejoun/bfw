using Const;
using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(AccountAsset), 
        menuName = MenuName.Assets + nameof(AccountAsset))]
    public class AccountAsset : FieldAsset<Account>, IAccount
    {
        public string AddressHex => Field.AddressHex ?? string.Empty;
        public string PrivateKey => Field.PrivateKey ?? string.Empty;
    }
}
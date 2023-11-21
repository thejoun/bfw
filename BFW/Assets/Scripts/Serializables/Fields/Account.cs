using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class Account : IAccount
    {
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string AddressHex { get; private set; }
        
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string PrivateKey { get; private set; }
    }
}
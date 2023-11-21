using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class Account : IAccount
    {
        [FormerlySerializedAs("<Address>k__BackingField")]
        [LabelWidth(ShortLabel.Width)] 
        [SerializeField] private string address;
        
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string PrivateKey { get; private set; }
        
        public string AddressHex => address;

    }
}
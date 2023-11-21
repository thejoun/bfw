using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class Address : IAddress
    {
        [FormerlySerializedAs("<Address>k__BackingField")]
        [LabelWidth(ShortLabel.Width)] 
        [SerializeField] private string address;
        
        public string AddressHex => address;
    }
}
using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class Contract : IContract
    {
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string Title { get; private set; }

        [FormerlySerializedAs("<Address>k__BackingField")]
        [LabelWidth(ShortLabel.Width)] 
        [SerializeField] private string address;
        
        [field: LabelWidth(ShortLabel.Width)]
        [field: TextArea(2, 50)]
        [field: SerializeField] public string Abi { get; private set; }

        public string AddressHex => address;
    }
}
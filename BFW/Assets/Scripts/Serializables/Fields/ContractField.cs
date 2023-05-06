using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class ContractField : IContract
    {
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string Title { get; private set; }
        
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string Address { get; private set; }
        
        [field: LabelWidth(ShortLabel.Width)]
        [field: TextArea(2, 50)]
        [field: SerializeField] public string Abi { get; private set; }
    }
}
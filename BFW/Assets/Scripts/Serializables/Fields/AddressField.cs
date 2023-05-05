using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class AddressField : IAddress
    {
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string Address { get; private set; }
    }
}
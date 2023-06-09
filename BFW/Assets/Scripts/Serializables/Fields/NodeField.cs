﻿using System;
using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class NodeField : INode
    {
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeField] public string Url { get; private set; }
    }
}
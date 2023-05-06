﻿using Core;
using Interfaces;
using Serializables;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(NodeAsset), 
        menuName = MenuName.Assets + nameof(NodeAsset))]
    public class NodeAsset : FieldAsset<NodeField>, INode
    {
        public string Url => Field.Url ?? string.Empty;
    }
}
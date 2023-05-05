using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class NodeAssetReference : INode
    {
        [AssetSelector] [HideLabel]
        [SerializeField] private NodeAsset node;

        public string Url => node.Url ?? string.Empty;
    }
}
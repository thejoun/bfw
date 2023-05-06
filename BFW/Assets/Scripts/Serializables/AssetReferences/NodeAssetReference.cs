using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class NodeAssetReference : AssetReference<NodeAsset>, INode
    {
        public string Url => Asset.Url ?? string.Empty;
    }
}
using Core;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = nameof(NodeAsset), 
        menuName = MenuName.Assets + nameof(NodeAsset))]
    public class NodeAsset : ScriptableObject, INode
    {
        [LabelWidth(ShortLabel.Width)] [HideLabel]
        [SerializeField] private NodeField node;

        public string Url => node.Url ?? string.Empty;
    }
}
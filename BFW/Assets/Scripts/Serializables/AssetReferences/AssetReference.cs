using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public abstract class AssetReference<TAsset>
    {
        [AssetSelector] [HideLabel] 
        [SerializeField] private TAsset asset;

        protected TAsset Asset => asset;
    }
}
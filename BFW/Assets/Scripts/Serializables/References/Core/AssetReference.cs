using System;
using Sirenix.OdinInspector;
using Unimorph.Core;
using UnityEngine;

namespace Serializables
{
    [PrettyName("Asset")]
    [Serializable] [InlineProperty]
    public abstract class AssetReference<TAsset>
    {
        [AssetSelector] [HideLabel] 
        [SerializeField] private TAsset asset;

        protected TAsset Asset => asset;
    }
}
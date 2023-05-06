﻿using System;
using Assets;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class ContractAssetReference : AssetReference<ContractAsset>, IContract
    {
        public string Title => Asset.Title ?? string.Empty;
        public string Address => Asset.Address ?? string.Empty;
        public string Abi => Asset.Abi ?? string.Empty;
    }
}
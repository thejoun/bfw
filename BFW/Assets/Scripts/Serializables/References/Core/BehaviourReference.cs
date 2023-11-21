using System;
using Sirenix.OdinInspector;
using Unimorph.Core;
using UnityEngine;

namespace Serializables
{
    [PrettyName("Behavior")]
    [Serializable] [InlineProperty]
    public abstract class BehaviourReference<T>
    {
        [HideLabel] 
        [SerializeField] protected T behaviour;
    }
}
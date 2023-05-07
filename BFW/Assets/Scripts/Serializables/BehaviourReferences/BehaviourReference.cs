using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public abstract class BehaviourReference<T>
    {
        [HideLabel] 
        [SerializeField] protected T behaviour;
    }
}
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
        [SerializeField] private T value;

        protected T Value => value;
        
        public BehaviourReference<T> WithValue(T value)
        {
            this.value = value;

            return this;
        }
    }
}
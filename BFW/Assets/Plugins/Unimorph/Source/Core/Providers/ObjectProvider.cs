using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unimorph.Core
{
    /// <summary>
    /// Base for references to Unity Objects.
    /// </summary>
    [Serializable]
    public abstract class ObjectProvider<T> : IProvider<T>
        where T : Object
    {
        [HideLabel] 
        [SerializeField] private T value;
        
        public T Value => value;
    }
}
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unimorph.Core
{
    /// <summary>
    /// Provider for primitive types and collections thereof.
    /// </summary>
    [PrettyName("Raw")]
    [Serializable]
    public abstract class RawProvider<T> : IProvider<T>
    {
        [HideLabel] 
        [SerializeField] private T value;
        
        public T Value => value;
    }

    public class RawBool : RawProvider<bool>, IBoolProvider {}
    public class RawInt : RawProvider<int>, IIntProvider {}
    public class RawFloat : RawProvider<float>, IFloatProvider {}
    public class RawString : RawProvider<string>, IStringProvider {}
    public class RawSprite : RawProvider<Sprite>, ISpriteProvider {}
    public class RawVector2 : RawProvider<Vector2>, IVector2Provider {}
    public class RawVector3 : RawProvider<Vector3>, IVector3Provider {}
    
    [Serializable]
    public class RawColor : IColorProvider
    {
        [HideLabel] 
        [SerializeField] private Color value = Color.white;
        
        public Color Value => value;
    }
}
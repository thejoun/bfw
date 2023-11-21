using System.Collections.Generic;
using UnityEngine;

namespace Unimorph.Core
{
	public interface IProvider<out T>
	{
		T Value { get; }
	}
	
	public interface IIntProvider : IProvider<int> { }
	public interface IFloatProvider : IProvider<float> { }
	public interface IDoubleProvider : IProvider<float> { }
	
	public interface IBoolProvider : IProvider<bool>, ICondition
	{
		bool ICondition.Check() => Value;
	}
	
	public interface IStringProvider : IProvider<string> { }

	public interface IVector2Provider : IProvider<Vector2> { }
	public interface IVector3Provider : IProvider<Vector3> { }

	public interface IColorProvider : IProvider<Color> { }
	
	public interface ISpriteProvider : IProvider<Sprite> { }

	public interface IEnumerableProvider<out T> : IProvider<IEnumerable<T>> { }
	public interface IListProvider<T> : IProvider<IList<T>> { }
}
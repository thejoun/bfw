using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unimorph.Core
{
	[ManualInject] 
	[InlineProperty] 
	[Serializable]
	public class Optional<T> : IOptional<T>, IProvider<T>
	{
		[HideLabel]
		[HorizontalGroup(Width = 20)]
		[SerializeField] private bool isEnabled;
		
		[HideLabel]
		[HorizontalGroup]
		[ShowIf(nameof(HasCondition))]
		[SerializeField] private T value;
		
		public bool HasCondition
		{
			get => isEnabled;
			set => isEnabled = value;
		}
		
		public T Value
		{
			get => HasCondition ? value : default;
			set => this.value = value;
		}
		
		public static implicit operator bool(Optional<T> field) => field.HasCondition;
		public static implicit operator T(Optional<T> field) => field.Value;

		public Optional<T> Enabled()
		{
			HasCondition = true;
			return this;
		}

		public Optional<T> WithValue(T value)
		{
			Value = value;
			return this;
		}

		public bool TryGetValue(out T value)
		{
			value = Value;
			return HasCondition;
		}
	}
}
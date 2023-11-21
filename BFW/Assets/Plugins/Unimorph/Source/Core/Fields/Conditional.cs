using System;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Unimorph.Core
{
	[ManualInject]
	[InlineProperty]
	[Serializable]
	public class Conditional<T> : IOptional<T>, IProvider<T>
	{
		[HideLabel]
		[HorizontalGroup]
		[SerializeField] private T value;
		
		[LabelWidth(20)]
		[LabelText("if")]
		[HorizontalGroup(Width = 40)]
		[SerializeField] private bool hasCondition;

		[HideLabel]
		[ShowIf(nameof(HasCondition))]
		[SerializeField] private Reference<ICondition> condition = new();

		public bool HasCondition
		{
			get => hasCondition;
			set => hasCondition = value;
		}
		
		public T Value
		{
			get => IsFulfilled ? value : default;
			set => this.value = value;
		}
		
		public ICondition Condition
		{
			get => HasCondition ? condition.Value : null;
			set => condition.Value = value;
		}
		
		private bool IsFulfilled => !HasCondition || (Condition?.Check() ?? false);

		
		public static implicit operator bool(Conditional<T> field) => field.HasCondition;
		public static implicit operator T(Conditional<T> field) => field.Value;

		public Conditional<T> WithValue(T value)
		{
			Value = value;
			return this;
		}
		
		public Conditional<T> WithCondition(ICondition condition)
		{
			HasCondition = true;
			Condition = condition;
			return this;
		}

		public bool TryGetValue(out T value)
		{
			value = Value;
			return IsFulfilled;
		}
	}
}
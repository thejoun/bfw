using System;
using Sirenix.OdinInspector;
using Unimorph.Core;
using UnityEngine;

namespace Unimorph.Fields
{
	[ManualInject] 
	[InlineProperty] 
	[Serializable]
	public class Reference<T> : Polymorphic<T>, IProvider<T>
	{
		[HideLabel] 
		[InlineProperty] 
		[HorizontalGroup] 
		[HideReferenceObjectPicker]
		[SerializeReference] private T value;

		public T Value
		{
			get => value;
			set => this.value = value;
		}

		public static implicit operator T(Reference<T> field) => field.Value;

		public Reference()
		{
			
		}
		
		public Reference(T value)
		{
			this.value = value;
		}
		
		protected override void OnTypeSelected(T instance)
		{
			value = instance;
		}
	}
}
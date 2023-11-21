using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unimorph.Reflection
{
	[Serializable] [InlineProperty]
	public class ReflectedMember
	{
		public enum SourceVariant
		{
			None,
			Field,
			Property
		}

		[HorizontalGroup]
		[HideLabel]
		[SerializeField] private Object target;
		
		[HorizontalGroup]
		[HideLabel]
		[ShowIf(nameof(IsTargeted))]
		[OnValueChanged(nameof(OnValueChanged))]
		[ValueDropdown(nameof(GetDropdownItems))]
		[SerializeField] private string name;
		
		private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Public
		                                          | System.Reflection.BindingFlags.NonPublic
		                                          | System.Reflection.BindingFlags.Instance
		                                          | System.Reflection.BindingFlags.Static
		                                          | System.Reflection.BindingFlags.FlattenHierarchy;

		public SourceVariant Variant { get; private set; }
		
		public FieldInfo Field { get; private set; }
		public PropertyInfo Property { get; private set; }

		public string Name
		{
			get => name;
			set { name = value; Field = null; Property = null; }
		}
		
		public Object Target
		{
			get => target;
			set => target = value;
		}
		
		public MemberFilterAttribute Filter { get; set; }

		private MemberInfo Member => Variant == SourceVariant.Field ? Field : Property; 
		
		public bool IsTargeted => Target;
		public bool IsAssigned => IsTargeted && !string.IsNullOrEmpty(Name);

		public ReflectedMember() { }

		public ReflectedMember(Component target, string name)
		{
			Name = name;
			Target = target;

			Reflect();
		}

		private void OnValueChanged()
		{
			Field = null;
			Property = null;
		}
		
		private IEnumerable<ValueDropdownItem> GetDropdownItems()
		{
			var items = IsTargeted 
				? MemberSelector.GetItemsIncludingNull(Target.GetType(), Filter) 
				: Enumerable.Empty<ValueDropdownItem>();

			return items;
		}
		
		public void Reflect()
		{
			EnsureAssigned();

			Field = null;
			Property = null;
			
			Variant = SourceVariant.None;

			var type = Target.GetType();
			
			const MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property;

			var members = type.GetMember(Name, memberTypes, BindingFlags);
			var member = members.FirstOrDefault();

			if (member is null)
			{
				throw new ReflectionException($"No matching field, property or method found: '{type.Name}.{name}'");
			}

			Variant = member.MemberType switch
			{
				MemberTypes.Field => SourceVariant.Field,
				MemberTypes.Property => SourceVariant.Property,
				_ => throw new ReflectionException()
			};

			switch (Variant)
			{
				case SourceVariant.Field: Field = (FieldInfo)member; break;
				case SourceVariant.Property: Property = (PropertyInfo)member; break;
				default: throw new ReflectionException();
			}
		}
		
		public void EnsureReflected()
		{
			if (Member is null) Reflect();
		}
		
		public void EnsureAssigned()
		{
			if (!IsAssigned)
			{
				throw new ReflectionException("Member hasn't been properly assigned.");
			}
		}
		
		public object Get()
		{
			EnsureReflected();

			return Variant switch
			{
				SourceVariant.Field => Field.GetValue(Target),
				SourceVariant.Property => Property.GetValue(Target),
				_ => throw new ReflectionException()
			};
		}
		
		public T Get<T>()
		{
			return (T)Get();
		}
		
		public void Set(object value)
		{
			EnsureReflected();

			switch (Variant)
			{
				case SourceVariant.Field: Field.SetValue(Target, value); break;
				case SourceVariant.Property: Property.SetValue(Target, value, null); break;
				default: throw new ReflectionException();
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unimorph.Reflection
{
	/// <summary>
	/// Filters the list of members.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
	public class MemberFilterAttribute : Attribute
	{
		public bool Fields { get; set; } = true;
		public bool Properties { get; set; } = true;

		public bool Gettable { get; set; } = true;
		public bool Settable { get; set; } = true;
		
		public bool Instance { get; set; } = true;
		public bool Static { get; set; } = true;
		
		public bool Public { get; set; } = true;
		public bool NonPublic { get; set; } = true;
		
		public bool ReadOnly { get; set; } = true;
		public bool WriteOnly { get; set; } = true;

		public IEnumerable<Type> Types { get; set; } = Enumerable.Empty<Type>();

		public MemberFilterAttribute()
		{
			
		}
		
		public MemberFilterAttribute(params Type[] types)
		{
			Types = types;
		}
		
		public MemberFilterAttribute(IEnumerable<Type> types)
		{
			Types = types;
		}

		public BindingFlags BindingFlags
		{
			get
			{
				BindingFlags flags = 0;

				if (Instance) flags |= BindingFlags.Instance;
				if (Static) flags |= BindingFlags.Static;
				
				if (Public) flags |= BindingFlags.Public;
				if (NonPublic) flags |= BindingFlags.NonPublic;

				return flags;
			}
		}
		
		public bool ValidateMember(MemberInfo member)
		{
			var valid = true;
			
			if (member is FieldInfo field)
			{
				valid &= ValidateMemberType(field.FieldType);
				
				if (!ReadOnly) valid &= !field.IsLiteral || !field.IsInitOnly;
			}
			else if (member is PropertyInfo property)
			{
				valid &= ValidateMemberType(property.PropertyType);

				if (!ReadOnly || (!Properties && Settable)) valid &= property.CanWrite;
				if (!WriteOnly || (!Properties && Gettable)) valid &= property.CanRead;
			}

			return valid;
		}
		
		private bool ValidateMemberType(Type type)
		{
			var valid = Types.Any(allowedType => allowedType.IsAssignableFrom(type));

			return valid;
		}
	}
}
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace Unimorph.Reflection
{
    public static class MemberSelector
    {
        private const BindingFlags DefaultBindingFlags =
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public static IEnumerable<ValueDropdownItem> GetItemsIncludingNull(Type targetType, 
            MemberFilterAttribute filter = null)
        {
            
            var items = GetItems(targetType, filter);
            
            foreach (var item in items)
            {
                yield return item;
            }

            var nullItem = new ValueDropdownItem("null", null);

            yield return nullItem;
        }

        private static IEnumerable<ValueDropdownItem> GetItems(Type targetType, 
            MemberFilterAttribute filter = null)
        {
            var entries = GetEntries(targetType, filter);

            var items = entries
                .Select(value => new ValueDropdownItem(value.Item1, value.Item2));
            
            return items;
        }

        private static IEnumerable<(string itemName, string memberName)> GetEntries(Type targetType, 
            MemberFilterAttribute filter = null)
        {
            var flags = filter?.BindingFlags ?? DefaultBindingFlags;

            var entries = GetAllTypes(targetType)
                .SelectMany(type => GetFieldsAndProperties(type, flags)
                    .Where(member => filter?.ValidateMember(member) ?? true)
                    .Select(member => GetEntry(type, member))
                    .OrderBy(entry => entry.label));

            return entries;
        }

        private static (string label, string name) GetEntry(Type targetType, MemberInfo member)
        {
            string typeName;
            string memberName;

            if (member is FieldInfo field)
            {
                typeName = field.FieldType.Name;
                memberName = field.Name;
            }
            else if (member is PropertyInfo property)
            {
                typeName = property.PropertyType.Name;
                memberName = property.Name;
            }
            else
            {
                throw new ReflectionException();
            }

            var containingTypeName = targetType.Name;
            
            var label = $"{containingTypeName}/{memberName} ({typeName})";
            var name = memberName;

            return (label, name);
        }

        private static IEnumerable<Type> GetAllTypes(Type type)
        {
            yield return type;

            foreach (var baseType in type.GetBaseTypes())
            {
                yield return baseType;
            }
        }

        private static IEnumerable<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags flags)
        {
            flags |= BindingFlags.DeclaredOnly;
            
            foreach (var field in type.GetFields(flags))
            {
                yield return field;
            }

            foreach (var property in type.GetProperties(flags))
            {
                yield return property;
            }
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unimorph.Core
{
    public static class TypeSelector
    {
        public static OdinSelector<object> Open(Type type, Vector2 topRightCornerPosition)
        {
            const int width = 250;
            const string title = "Type";

            var items = GetItemsIncludingNull(type);

            return Open(items, topRightCornerPosition, width, title);
        }

        private static OdinSelector<object> Open(IEnumerable<ValueDropdownItem> items, Vector2 topRightCornerPosition, 
            int width, string title)
        {
            var selector = Create(title, items);

            var position = topRightCornerPosition + Vector2.left * width;

            selector.ShowInPopup(position, width);

            return selector;
        }

        private static GenericSelector<object> Create(string title, IEnumerable<ValueDropdownItem> items)
        {
            var selectorItems = items
                .Select(item => new GenericSelectorItem<object>(item.Text, item.Value));

            var selector = new GenericSelector<object>(title, false, selectorItems);

            selector.EnableSingleClickToSelect();

            var selection = Enumerable.Empty<object>();

            selector.SetSelection(selection);

            selector.SelectionTree.Config.DrawSearchToolbar = true;

            selector.SelectionTree.EnumerateTree().AddThumbnailIcons(true);

            return selector;
        }

        private static IEnumerable<ValueDropdownItem> GetItemsIncludingNull(Type baseType)
        {
            var items = GetItems(baseType);
            
            foreach (var item in items)
            {
                yield return item;
            }

            var nullItem = new ValueDropdownItem("null", null);

            yield return nullItem;
        }

        private static IEnumerable<ValueDropdownItem> GetItems(Type baseType)
        {
            var values = GetValues(baseType);

            var items = values
                .Select(item => new ValueDropdownItem(item.Item1, item.Item2));

            items = items.OrderBy(item => item.Text);

            return items;
        }

        private static IEnumerable<(string, TypeInfo)> GetValues(Type baseType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = assemblies
                .SelectMany(assembly => assembly.DefinedTypes)
                .Where(type => DerivesFromOrEqual(type, baseType))
                .Where(type => !type.IsInterface && !type.IsInterface)
                .Where(type => !type.IsGenericType)
                .Where(type => !DerivesFromOrEqual(type, typeof(Object)));

            var dropdown = types
                .Select(type => (GetPrettyName(type), type));

            return dropdown;
        }
        
        private static bool DerivesFromOrEqual(Type a, Type b)
        {
            return b == a || b.IsAssignableFrom(a);
        }

        private static string GetPrettyName(Type type)
        {
            var attribute = GetAttributeInherited(type);

            return attribute?.Name ?? type.Name;
        }

        private static PrettyNameAttribute GetAttributeInherited(Type type)
        {
            PrettyNameAttribute attribute = null;

            var currentType = type;

            while (attribute is null && currentType is not null)
            {
                attribute = currentType.GetAttribute<PrettyNameAttribute>();

                currentType = currentType.BaseType;
            }

            return attribute;
        }
    }
}
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unimorph.Core
{
    [InlineProperty]
    [Serializable]
    public abstract class Polymorphic<T>
    {
        [HorizontalGroup(TypeButton.Width)]
        [Button(TypeButton.Text)]
        private void Switch() => SwitchType();

        protected abstract void OnTypeSelected(T instance);

        protected void SwitchType()
        {
#if UNITY_EDITOR
            var type = typeof(T);
            var position = Event.current.mousePosition;

            var selector = TypeSelector.Open(type, position);

            selector.SelectionConfirmed += OnItemsSelected;
#endif
        }

        private void OnItemsSelected(IEnumerable<object> selection)
        {
            var types = selection.OfType<TypeInfo>();
            var type = types.FirstOrDefault();

            OnTypeSelected(type);
        }

        private void OnTypeSelected(TypeInfo type)
        {
            var instance = type != null ? (T)Activator.CreateInstance(type) : default;

            OnTypeSelected(instance);
        }
    }
}
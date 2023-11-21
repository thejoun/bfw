using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using Unimorph.Core;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Unimorph.Injection
{
    /// <summary>
    /// Inject the whole ProjectContext into any non-Unity object at both runtime and edit-time 
    /// </summary>
    public static class ManualInject
    {
        private static readonly List<Type> IgnoredBaseTypes = new()
        {
            typeof(ScriptableObject), typeof(MonoBehaviour), typeof(Behaviour), typeof(Component), 
            typeof(Object)
        };
        
        private static readonly List<string> IgnoredFieldNamespaces = new()
        {
            "Sirenix.Utilities"
        };

        public static void Into(params object[] instances)
        {
            Into(instances.AsEnumerable());
        }

        public static void Into(IEnumerable<object> instances)
        {
            instances.ForEach(Into);
        }

        /// <summary>
        /// Inject dependencies into any object, including non-MonoBehaviors.
        /// Should handle all [Inject] attributes in a standard way.
        /// Uses either ProjectContext or the custom container.
        /// </summary>
        public static void Into(object instance)
        {
            if (instance is null)
            {
                return;
            }

            if (instance is IEnumerable<object> enumerable)
            {
                Into(enumerable);
                return;
            }
            
            EditorContext.Container.Inject(instance);

            RegisterTickable(instance);
            
            InjectIntoMembersRecursive(instance);
        }

        /// <summary>
        /// Recursively inject into members that are marked with an attribute.
        /// </summary>
        private static void InjectIntoMembersRecursive(object injectable)
        {
            var type = injectable.GetType();
            
            InjectIntoMembersRecursive(injectable, type);
        }

        private static void InjectIntoMembersRecursive(object injectable, Type type)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var fields = type.GetFields(flags);

            foreach (var field in fields)
            {
                InjectIntoFieldRecursive(injectable, field);
            }

            var baseType = type.BaseType;
            
            if (baseType != null && !IgnoredBaseTypes.Contains(baseType))
            {
                InjectIntoMembersRecursive(injectable, baseType);
            }
        }

        private static void InjectIntoFieldRecursive(object injectable, FieldInfo field)
        {
            var inject = false;

            try
            {
                inject = CanInjectInto(injectable, field);
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while injecting into '{field}'\n{e}\n");
            }

            if (inject)
            {
                var instance = field.GetValue(injectable);

                Into(instance);
            }
        }

        private static bool CanInjectInto(object injectable, FieldInfo field)
        {
            var inject = false;
            
            var fieldType = field.FieldType;

            if (IsIgnored(fieldType))
            {
                return false;
            }
            
            var value = field.GetValue(injectable);
            var hasValue = value is not null;
            var isEnumerable = fieldType.InheritsFrom<IEnumerable>();

            inject |= CanInjectInto(field);
            inject |= CanInjectInto(fieldType);

            if (hasValue)
            {
                var valueType = value.GetType();

                inject |= CanInjectInto(valueType);
            }

            if (isEnumerable)
            {
                var genericTypes = fieldType.GenericTypeArguments;

                foreach (var genericType in genericTypes)
                {
                    inject |= CanInjectInto(genericType);
                }
            }

            if (hasValue && isEnumerable)
            {
                var enumerableValues = (IEnumerable) value;

                foreach (var enumerableValue in enumerableValues)
                {
                    if (enumerableValue is not null)
                    {
                        var enumerableValueType = enumerableValue.GetType();

                        inject |= CanInjectInto(enumerableValueType);
                    }
                }
            }

            return inject;
        }

        private static bool CanInjectInto(MemberInfo member)
        {
            return member.GetAttribute<ManualInjectAttribute>() is not null;
        }

        private static bool IsIgnored(Type type)
        {
            if (IgnoredFieldNamespaces.Any(ns => type.Namespace?.StartsWith(ns) ?? false))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Register an object in Zenject's TickableManager
        /// </summary>
        private static void RegisterTickable(object instance)
        {
            if (!ProjectContext.HasInstance)
            {
                // only register tickables in play mode
                return;
            }

            if (instance is ITickable or IFixedTickable or ILateTickable)
            {
                var manager = EditorContext.Container.TryResolve<TickableManager>();
                var typeName = instance.GetType().Name;

                if (manager is null)
                {
                    Debug.LogError($"Couldn't resolve TickableManager " +
                                   $"when injecting into '{typeName}'.");
                    return;
                }

                if (instance is ITickable tickable) manager.Add(tickable);
                if (instance is IFixedTickable fixedTickable) manager.AddFixed(fixedTickable);
                if (instance is ILateTickable lateTickable) manager.AddLate(lateTickable);
            }
        }
    }
}
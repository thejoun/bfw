using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Unimorph.Core
{
    [ManualInject]
    [PrettyName(TypeCategory.Core + "Composite")]
    [Serializable]
    public class CompositeInvoke : IInvokable
    {
        [LabelText("All")]
        [SerializeField] private List<Reference<IInvokable>> invokables = new();

        public void Invoke()
        {
            foreach (var invokable in invokables)
            {
                invokable.Value?.Invoke();
            }            
        }
    }
}
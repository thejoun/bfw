using System;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Unimorph.Core
{
    [ManualInject]
    [PrettyName(TypeCategory.Core + "Conditional")]
    [Serializable]
    public class ConditionalInvoke : IInvokable
    {
        [LabelText("if")] [LabelWidth(20)]
        [SerializeField] private Reference<ICondition> condition;
        [LabelText("do")] [LabelWidth(20)]
        [SerializeField] private Reference<IInvokable> invoke;
        
        public void Invoke()
        {
            if (condition.Value?.Check() ?? false)
            {
                invoke.Value?.Invoke();
            }
        }
    }
}
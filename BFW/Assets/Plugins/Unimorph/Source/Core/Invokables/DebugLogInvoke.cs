using System;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Unimorph.Core
{
    [ManualInject]
    [PrettyName(TypeCategory.General + "Debug Log")]
    [Serializable]
    public class DebugLogInvoke : IInvokable
    {
        [LabelText("debug log")] [LabelWidth(60)]
        [SerializeField] private Reference<IStringProvider> log;
        
        public void Invoke()
        {
            Debug.Log(log.Value?.Value);
        }
    }
}
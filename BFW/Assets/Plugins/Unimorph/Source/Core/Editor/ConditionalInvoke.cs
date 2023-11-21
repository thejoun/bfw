using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unimorph.Fields;
using Unimorph.Injection;
using UnityEditor;
using UnityEngine;

namespace Unimorph.Core.Editor
{
    public class ConditionalInvoke : OdinEditorWindow
    {
        [SerializeField] private Conditional<Reference<IInvokable>> invoke;

        [MenuItem("Tools/Unimorph/Conditional Invoke")]
        private static void OpenWindow()
        {
            var window = GetWindow<ConditionalInvoke>();
            
            window.Show();
        }

        [Button]
        private void Invoke()
        {
            ManualInject.Into(invoke);
            
            if (invoke)
            {
                invoke.Value.Value?.Invoke();
            }
        }
    }
}
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unimorph.Fields;
using Unimorph.Injection;
using UnityEditor;
using UnityEngine;

namespace Unimorph.Core.Editor
{
    public class OptionalInvoke : OdinEditorWindow
    {
        [SerializeField] private Optional<Reference<IInvokable>> invoke;

        [MenuItem("Tools/Unimorph/Optional Invoke")]
        private static void OpenWindow()
        {
            var window = GetWindow<OptionalInvoke>();
            
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
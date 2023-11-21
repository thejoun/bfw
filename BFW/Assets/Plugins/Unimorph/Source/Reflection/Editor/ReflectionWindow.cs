using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Unimorph.Reflection.Editor
{
    public class ReflectionWindow : OdinEditorWindow
    {
        [MemberFilter(typeof(string))]
        [SerializeField] private ReflectedMember member;

        [MenuItem("Tools/Unimorph/Reflection")]
        private static void OpenWindow()
        {
            var window = GetWindow<ReflectionWindow>();
            
            window.Show();
        }

        [Button]
        private void Print()
        {
            Debug.Log(member.Get());
        }
    }
}
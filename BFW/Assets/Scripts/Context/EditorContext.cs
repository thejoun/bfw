using Core;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Context
{
    [CreateAssetMenu(fileName = nameof(EditorContext), 
        menuName = MenuName.Context + nameof(EditorContext))]
    public class EditorContext : ScriptableObject
    {
        [field: HideReferenceObjectPicker]
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeReference] public IAccount Account { get; private set; }
        
        [field: HideReferenceObjectPicker]
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeReference] public INode Node { get; private set; }

        /// <summary>
        /// Beware of the singleton!
        /// </summary>
        public static EditorContext Instance
        {
            get
            {
#if UNITY_EDITOR
                var path = "Assets/Scriptables/Context/EditorContext.asset";
                var asset = AssetDatabase.LoadAssetAtPath<EditorContext>(path);

                if (!asset)
                {
                    Debug.LogError($"No EditorContext asset found at {path}");
                }
                
                return asset;
#endif
                return null;
            }
        }
    }
}
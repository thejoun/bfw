using Core;
using Interfaces;
using Objects;
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

        [field: HideReferenceObjectPicker]
        [field: LabelWidth(ShortLabel.Width)]
        [field: SerializeReference] public IContract Uint256Abi { get; private set; }

        private MyWeb _web;
        
        /// <summary>
        /// Beware of the singleton! Only for prototyping!
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
        
        /// <summary>
        /// Beware of the singleton! Only for prototyping!
        /// </summary>
        public MyWeb Web
        {
            get
            {
                if (_web == null
                    || _web.Account == Account
                    || _web.Node == Node)
                {
                    _web = new MyWeb(Account, Node);
                }

                return _web;
            }
        }
    }
}
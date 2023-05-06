using Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    public abstract class FieldAsset<TField> : ScriptableObject
    {
        [LabelWidth(ShortLabel.Width)] [HideLabel]
        [SerializeField] private TField field;

        protected TField Field => field;
    }
}
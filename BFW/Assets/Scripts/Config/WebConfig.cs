using Core;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(WebConfig), menuName = MenuName.Config + nameof(WebConfig))]
    public class WebConfig : ScriptableObject
    {
        [field: SerializeField] public int GasLimit { get; private set; }        
    }
}
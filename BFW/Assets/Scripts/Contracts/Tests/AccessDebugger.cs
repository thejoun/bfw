using Context;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class AccessDebugger : MonoBehaviour
    {
        [SerializeReference] private IContract whoHas;
        [SerializeReference] private IContract toWhere;

        private EcsWeb Web => EditorContext.Instance.Web;
        
        [Button]
        private async void Check()
        {
            await Web.Ecs.HasAccess(whoHas, toWhere);
        }
    }
}
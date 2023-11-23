using Interfaces;
using UnityEngine;
using Zenject;

namespace ECS.Core
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [Inject] protected IContract Contract;
        [Inject] protected IEntity Entity;
        
        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
    }
}
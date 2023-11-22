using Interfaces;
using Unimorph.Injection;
using UnityEngine;
using Zenject;

namespace ECS.Core
{
    [ExecuteAlways]
    public abstract class EntityComponent : MonoBehaviour
    {
        [Inject] private IContract contract;
        [Inject] private IEntity entity;

        protected IEntity Entity => entity;

        private void OnEnable()
        {
            ManualInject.Into(this);
            
            
        }

        private void OnDisable()
        {
            
        }
    }
}
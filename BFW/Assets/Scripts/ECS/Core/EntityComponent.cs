using ECS.Entities;
using Interfaces;
using Serializables;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [Inject] private IContract contract;
        [Inject] private IEntity entity;
        
        // [HideReferenceObjectPicker]
        // [SerializeReference] private IEntity entity;
        
        public IEntity Entity => entity;

        protected virtual void Reset()
        {
            Init();
        }
        
        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            entity = new EntityReference(GetComponent<Entity>());
        }
    }
}
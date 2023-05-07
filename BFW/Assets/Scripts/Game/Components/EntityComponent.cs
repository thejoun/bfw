using Game.Entities;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Components
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [HideReferenceObjectPicker]
        [SerializeReference] private IEntity entity;
        
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
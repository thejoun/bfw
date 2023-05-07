using System;
using Game.Entities;
using Interfaces;
using Serializables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Systems
{
    public abstract class EntitySystem : EcsSystem
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
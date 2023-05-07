using System;
using Config;
using Game.Entities;
using Interfaces;
using Serializables;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public abstract class EcsSystem : MonoBehaviour
    {
        [Inject] protected IMyWeb web;
        [Inject] protected IAccount account;
        [Inject] protected WebConfig webConfig;
        
        [SerializeReference] private IEntity entity;

        protected int GasLimit => webConfig.GasLimit;
        
        public IEntity Entity => entity;

        public Action Success;
        public Action Failed;
        
        protected virtual void Reset()
        {
            entity = new EntityReference(GetComponent<RemoteEntity>());
        }
    }
}
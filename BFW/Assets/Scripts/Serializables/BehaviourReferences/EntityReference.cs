using System;
using System.Numerics;
using Game.Entities;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class EntityReference : BehaviourReference<Entity>, IEntity
    {
        public BigInteger Id => behaviour.Id;
        public GameObject Go => behaviour.Go;

        public EntityReference(Entity entity)
        {
            behaviour = entity;
        }
    }
}
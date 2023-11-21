using System;
using System.Numerics;
using ECS.Entities;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class EntityReference : BehaviourReference<Entity>, IEntity
    {
        public BigInteger Id => behaviour.Id;
        public GameObject GameObject => behaviour.GameObject;

        public EntityReference(Entity entity)
        {
            behaviour = entity;
        }
    }
}
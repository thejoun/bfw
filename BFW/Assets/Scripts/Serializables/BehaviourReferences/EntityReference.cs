using System;
using System.Numerics;
using Game.Entities;
using Interfaces;
using Sirenix.OdinInspector;

namespace Serializables
{
    [Serializable] [InlineProperty]
    public class EntityReference : BehaviourReference<Entity>, IEntity
    {
        public BigInteger Id => behaviour.Id;

        public EntityReference(Entity entity)
        {
            behaviour = entity;
        }
    }
}
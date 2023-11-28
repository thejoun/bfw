using System;
using ECS.Entities;
using Interfaces;
using Sirenix.OdinInspector;

namespace Serializables.References
{
    [Serializable] [InlineProperty]
    public class EntityReference : BehaviourReference<Entity>, IHasEntity
    {
        public IEntity Entity => Value;
    }
}
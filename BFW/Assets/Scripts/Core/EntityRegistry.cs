using System.Linq;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class EntityRegistry : Registry<IEntity>, IEntityRegistry
    {
        private int highestReserved;
        
        public int GetNewId()
        {
            var maxId = !Values.Any() ? 0 : (int)Values.Max(entity => entity.Id);

            maxId = Mathf.Max(maxId, highestReserved);
            
            var newId = maxId + 1;

            highestReserved = newId;
            
            return newId;
        }
    }
}
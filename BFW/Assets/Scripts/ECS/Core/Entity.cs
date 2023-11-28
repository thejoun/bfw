using System.Numerics;
using Interfaces;
using UnityEngine;

namespace ECS.Entities
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private int id;

        public BigInteger Id => id;
        public GameObject GameObject => this ? gameObject : null;

        public void SetId(int id)
        {
            this.id = id;
        }
        
        public Entity WithId(int id)
        {
            this.id = id;

            return this;
        }
    }
}
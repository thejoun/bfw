using System.Numerics;
using Interfaces;
using UnityEngine;

namespace ECS.Entities
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private int id;

        public BigInteger Id => id;
        public GameObject GameObject => gameObject;

        public void SetId(int id)
        {
            this.id = id;
        }
    }
}
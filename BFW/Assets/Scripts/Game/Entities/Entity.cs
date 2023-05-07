using System.Numerics;
using Interfaces;
using UnityEngine;

namespace Game.Entities
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private int id;

        public BigInteger Id => id;

        public void SetId(int id)
        {
            this.id = id;
        }
    }
}
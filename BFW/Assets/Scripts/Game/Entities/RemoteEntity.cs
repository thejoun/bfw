using System.Numerics;
using Interfaces;
using UnityEngine;

namespace Game.Entities
{
    public abstract class RemoteEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private int id;

        public BigInteger Id => id;
    }
}
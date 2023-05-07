using Interfaces;
using UnityEngine;

namespace Game.Components
{
    public abstract class RemoteComponent : MonoBehaviour
    {
        [SerializeReference] private IEntity entity;
        
        public IEntity Entity => entity;
    }
}
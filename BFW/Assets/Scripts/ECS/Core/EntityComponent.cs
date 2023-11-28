using Interfaces;
using UnityEngine;
using Zenject;

namespace ECS.Core
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [Inject] protected IContract Contract;
        [Inject] protected IEntity Entity;
        [Inject] protected IInstantiator Instantiator;
    }
}
using Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class PositionComponent : PrimitiveComponent<Vector2Int>
    {
        [Inject] private IContract contract;
    }
}
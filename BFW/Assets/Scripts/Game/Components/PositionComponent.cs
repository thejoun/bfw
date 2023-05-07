using Helpers;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class PositionComponent : PrimitiveComponent<Vector2Int>
    {
        [Inject] private IContract contract;

        protected override void OnValueChanged(Vector2Int value)
        {
            base.OnValueChanged(value);

            var pos = HexGridHelper.HexPosition(value);

            Entity.Go.transform.position = pos;
        }
    }
}
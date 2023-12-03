using DG.Tweening;
using ECS.Core;
using Extensions;
using Helpers;
using UnityEngine;

namespace ECS.Components
{
    public class PositionComponent : ValueComponent<Vector2Int>
    {
        public override string ComponentName => "pos";

        protected override void OnValueChanged(byte[] bytes)
        {
            var position = bytes.ToVector2Int();
            
            SetValue(position);
        }

        protected override void OnValueChanged(Vector2Int value)
        {
            Debug.Log($"Position of entity {Entity.Id} changed to {value}");

            var pos = HexGridHelper.HexPosition(value);

            Entity.GameObject.transform.DOLocalMove(pos, 0.5f);
            
            base.OnValueChanged(value);
        }
    }
}
using DG.Tweening;
using ECS.Core;
using Extensions;
using Helpers;
using UnityEngine;

namespace ECS.Components
{
    public class PositionComponent : ValueComponent<Vector2Int>
    {
        protected override void OnValueChanged(byte[] bytes)
        {
            base.OnValueChanged(bytes);
            
            var position = bytes.ToVector2Int();
            
            OnValueChanged(position);
        }

        protected override void OnValueChanged(Vector2Int value)
        {
            base.OnValueChanged(value);
            
            Debug.Log($"Position of entity {Entity.Id} changed to {value}");

            var pos = HexGridHelper.HexPosition(value);

            Entity.GameObject.transform.DOLocalMove(pos, 0.5f);
        }
    }
}
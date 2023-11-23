using System;
using System.Linq;
using DG.Tweening;
using ECS.Core;
using Helpers;
using UnityEngine;
using UnityEngine.Assertions;

namespace ECS.Components
{
    public class PositionComponent : ValueComponent<Vector2Int>
    {
        protected override void OnValueChanged(byte[] bytes)
        {
            base.OnValueChanged(bytes);
            
            Assert.IsTrue(bytes.Length == 64);

            var bigBytesX = bytes[..32];
            var bigBytesY = bytes[32..];
            
            Assert.IsTrue(bigBytesX.Length == 32);
            Assert.IsTrue(bigBytesY.Length == 32);
            
            var smallBytesX = bigBytesX[28..];
            var smallBytesY = bigBytesY[28..];

            Assert.IsTrue(smallBytesX.Length == 4 && smallBytesY.Length == 4);

            var x = BitConverter.ToInt32(smallBytesX.Reverse().ToArray());
            var y = BitConverter.ToInt32(smallBytesY.Reverse().ToArray());

            Debug.Log($"Position of entity {Entity.Id} changed to ({x},{y})");

            var position = new Vector2Int(x, y);
            
            OnValueChanged(position);
        }

        protected override void OnValueChanged(Vector2Int value)
        {
            base.OnValueChanged(value);

            var pos = HexGridHelper.HexPosition(value);

            Entity.GameObject.transform.DOLocalMove(pos, 0.5f);
        }
    }
}
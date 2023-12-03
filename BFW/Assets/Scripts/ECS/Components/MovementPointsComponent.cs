using ECS.Core;
using Extensions;
using UnityEngine;

namespace ECS.Components
{
    public class MovementPointsComponent : ValueComponent<int>
    {
        public override string ComponentName => "movpts";

        protected override void OnValueChanged(byte[] bytes)
        {
            var value = bytes.ToInt();
            
            SetValue(value);
        }

        protected override void OnValueChanged(int value)
        {
            Debug.Log($"Movement points on entity {Entity.Id} changed to {value}");
            
            base.OnValueChanged(value);
        }
    }
}
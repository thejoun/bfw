using ECS.Core;
using Extensions;
using UnityEngine;

namespace ECS.Components
{
    public class MovementCostComponent : ValueComponent<int>
    {
        public override string ComponentName => "movcost";

        protected override void OnValueChanged(byte[] bytes)
        {
            var value = bytes.ToInt();
            
            SetValue(value);
        }

        protected override void OnValueChanged(int value)
        {
            Debug.Log($"Movement cost on entity {Entity.Id} changed to {value}");
            
            base.OnValueChanged(value);
        }
    }
}
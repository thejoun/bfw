using ECS.Core;
using Extensions;
using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public class TerrainComponent : InstantiatableValueComponent<int>
    {
        [Inject] private GameObject template;

        private SpriteRenderer spriteRenderer;

        public override string ComponentName => "terrain";

        protected override void Instantiate()
        {
            var instance = Instantiator.InstantiatePrefab(template, transform);

            instance.name = "Terrain";

            spriteRenderer = instance.GetComponent<SpriteRenderer>();
        }

        protected override void OnValueChanged(byte[] bytes)
        {
            var value = bytes.ToInt();
            
            SetValue(value);
        }

        protected override void OnValueChanged(int value)
        {
            Debug.Log($"Terrain on entity {Entity.Id} changed to {value}");

            var colorHash = new ColorHash().Rgb(value.ToString());
            
            var color = new Color(
                colorHash.R / 255f, 
                colorHash.G / 255f, 
                colorHash.B / 255f);

            spriteRenderer.color = color;
            
            base.OnValueChanged(value);
        }
    }
}
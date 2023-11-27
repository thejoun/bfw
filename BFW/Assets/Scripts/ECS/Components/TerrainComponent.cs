using ECS.Core;
using Extensions;
using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public class TerrainComponent : ValueComponent<int>
    {
        [Inject] private GameObject template;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            var instance = Instantiator.InstantiatePrefab(template, transform);

            instance.name = "Terrain";

            spriteRenderer = instance.GetComponent<SpriteRenderer>();
        }

        protected override void OnValueChanged(byte[] bytes)
        {
            base.OnValueChanged(bytes);

            var value = bytes.ToInt();
            
            OnValueChanged(value);
        }

        protected override void OnValueChanged(int value)
        {
            base.OnValueChanged(value);

            Debug.Log($"Terrain on entity {Entity.Id} changed to {value}");

            var colorHash = new ColorHash().Rgb(value.ToString());
            
            var color = new Color(
                colorHash.R / 255f, 
                colorHash.G / 255f, 
                colorHash.B / 255f);

            spriteRenderer.color = color;
        }
    }
}
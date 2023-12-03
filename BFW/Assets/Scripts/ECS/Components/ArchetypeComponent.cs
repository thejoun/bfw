using ECS.Core;
using Extensions;
using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public class ArchetypeComponent : InstantiatableValueComponent<int>
    {
        [Inject] private GameObject template;

        private SpriteRenderer spriteRenderer;

        public override string ComponentName => "archetype";

        protected override void Instantiate()
        {
            var instance = Instantiator.InstantiatePrefab(template, transform);

            instance.name = "Archetype";

            spriteRenderer = instance.GetComponent<SpriteRenderer>();
        }
        
        protected override void OnValueChanged(byte[] bytes)
        {
            var value = bytes.ToInt();
            
            SetValue(value);
        }

        protected override void OnValueChanged(int value)
        {
            Debug.Log($"Archetype on entity {Entity.Id} changed to {value}");
            
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
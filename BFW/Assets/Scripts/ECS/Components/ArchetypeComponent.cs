using ECS.Core;
using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public class ArchetypeComponent : ValueComponent<int>
    {
        [Inject] private SpriteRenderer spriteRenderer;

        public override string ComponentName => "archetype";

        protected override void OnValueChanged(byte[] bytes)
        {
            // todo
        }

        protected override void OnValueChanged(int value)
        {
            var colorHash = new ColorHash().Rgb(value.ToString());
            var color = new Color(
                colorHash.R / 255f, 
                colorHash.G / 255f, 
                colorHash.B / 255f);

            spriteRenderer.color = color;
        }
    }
}
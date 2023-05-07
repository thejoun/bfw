using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class ArchetypeComponent : PrimitiveComponent<int>
    {
        [Inject] private SpriteRenderer spriteRenderer;
        
        protected override void OnValueChanged(int value)
        {
            base.OnValueChanged(value);

            var colorHash = new ColorHash().Rgb(value.ToString());
            var color = new Color(colorHash.R, colorHash.G, colorHash.B);

            spriteRenderer.color = color;
        }
    }
}
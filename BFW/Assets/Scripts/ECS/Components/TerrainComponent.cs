﻿using ECS.Core;
using Fernandezja.ColorHashSharp;
using UnityEngine;
using Zenject;

namespace ECS.Components
{
    public class TerrainComponent : PrimitiveComponent<int>
    {
        [Inject] private SpriteRenderer spriteRenderer;
        
        protected override void OnValueChanged(int value)
        {
            base.OnValueChanged(value);

            var colorHash = new ColorHash().Rgb(value.ToString());
            var color = new Color(
                colorHash.R / 255f, 
                colorHash.G / 255f, 
                colorHash.B / 255f);

            spriteRenderer.color = color;
        }
    }
}
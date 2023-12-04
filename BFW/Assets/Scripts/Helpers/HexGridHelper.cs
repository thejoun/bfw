using UnityEngine;

namespace Helpers
{
    public static class HexGridHelper
    {
        public static Vector2 HexPosition(Vector2Int hex)
        {
            const float height = 1f;
            const float width = 1.732f;

            var x = hex.x * width / 2f;
            var y = hex.y * height;

            var mod = hex.x % 2;
            
            if (mod is 1 or -1)
            {
                y += height / 2f;
            }

            return new Vector2(x, y);
        }
    }
}
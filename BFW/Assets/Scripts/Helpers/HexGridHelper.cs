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
            
            if (hex.x % 2 == 1)
            {
                y += height / 2f;
            }

            return new Vector2(x, y);
        }
    }
}
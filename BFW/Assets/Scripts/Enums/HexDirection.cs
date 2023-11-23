using Structs;
using UnityEngine;

namespace Enums
{
    public enum HexDirection
    {
        None = 0,
        Up = 1,
        UpRight = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        UpLeft = 6
    }

    public static class HexDirectionExtensions
    {
        public static Vector2Int VectorFrom(this HexDirection direction, int currentX)
        {
            var even = currentX % 2 == 0;
            var odd = !even;
            
            (int x, int y) = direction switch
            {
                HexDirection.Up => (0, 1),
                HexDirection.UpRight => odd ? (1, 1) : (1, 0),
                HexDirection.DownRight => even ? (1, -1) : (1, 0),
                HexDirection.Down => (0, -1),
                HexDirection.DownLeft => even ? (-1, -1) : (-1, 0),
                HexDirection.UpLeft => odd ? (-1, 1) : (-1, 0),
                _ => (0, 0),
            };

            return new Vector2Int(x, y);
        }
    }
}
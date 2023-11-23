using System;
using System.Globalization;
using System.Numerics;
using UnityEngine;

namespace Structs
{
    public struct BigVector2Int : IEquatable<BigVector2Int>
    {
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }

        public BigVector2Int(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }
        
        public void Set(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }
        
        public static BigVector2Int Zero => new(0, 0);
        public static BigVector2Int One => new(1, 1);
        public static BigVector2Int Up => new(0, 1);
        public static BigVector2Int Down => new(0, -1);
        public static BigVector2Int Left => new(-1, 0);
        public static BigVector2Int Right => new(1, 0);
        
        public float Magnitude => Mathf.Sqrt((float)(X * X + Y * Y));

        public BigInteger SqrMagnitude => X * X + Y * Y;
        
        public static float Distance(BigVector2Int a, BigVector2Int b)
        {
            var num1 = (float)(a.X - b.X);
            var num2 = (float)(a.Y - b.Y);
            
            return (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);
        }

        public static BigVector2Int operator -(BigVector2Int v) => new(-v.X, -v.Y);

        public static BigVector2Int operator +(BigVector2Int a, BigVector2Int b) => new(a.X + b.X, a.Y + b.Y);

        public static BigVector2Int operator -(BigVector2Int a, BigVector2Int b) => new(a.X - b.X, a.Y - b.Y);

        public static BigVector2Int operator *(BigVector2Int a, BigVector2Int b) => new(a.X * b.X, a.Y * b.Y);

        public static BigVector2Int operator *(int a, BigVector2Int b) => new(a * b.X, a * b.Y);

        public static BigVector2Int operator *(BigVector2Int a, int b) => new(a.X * b, a.Y * b);

        public static BigVector2Int operator /(BigVector2Int a, int b) => new(a.X / b, a.Y / b);

        public static bool operator ==(BigVector2Int lhs, BigVector2Int rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;

        public static bool operator !=(BigVector2Int lhs, BigVector2Int rhs) => !(lhs == rhs);
        
        public override bool Equals(object other) => other is BigVector2Int other1 && this.Equals(other1);

        public bool Equals(BigVector2Int other) => X == other.X && Y == other.Y;
        
        public override int GetHashCode()
        {
            var num1 = X;
            var hashCode = num1.GetHashCode();
            num1 = Y;
            var num2 = num1.GetHashCode() << 2;
            return hashCode ^ num2;
        }
        
        public override string ToString() => ToString(null, null);
        
        public string ToString(string format) => ToString(format, null);
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
            formatProvider ??= CultureInfo.InvariantCulture.NumberFormat;

            return $"({X.ToString(format, formatProvider)}, {(object)Y.ToString(format, formatProvider)})";
        }
    }
}
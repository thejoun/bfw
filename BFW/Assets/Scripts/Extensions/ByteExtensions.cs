using System;
using System.Linq;
using System.Numerics;
using Structs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Extensions
{
    public static class ByteExtensions
    {
        public static BigVector2Int ToBigVector2Int(this byte[] bytes)
        {
            Assert.IsTrue(bytes.Length == 64);

            var bytesX = bytes[..32];
            var bytesY = bytes[32..];
            
            var x = bytesX.ToBigInteger();
            var y = bytesY.ToBigInteger();

            var vector = new BigVector2Int(x, y);

            return vector;
        }

        public static Vector2Int ToVector2Int(this byte[] bytes)
        {
            Assert.IsTrue(bytes.Length == 64);

            var bigBytesX = bytes[..32];
            var bigBytesY = bytes[32..];
            
            Assert.IsTrue(bigBytesX.Length == 32);
            Assert.IsTrue(bigBytesY.Length == 32);
            
            var smallBytesX = bigBytesX[28..];
            var smallBytesY = bigBytesY[28..];

            var x = smallBytesX.ToInt();
            var y = smallBytesY.ToInt();
            
            var vector = new Vector2Int(x, y);

            return vector;
        }

        public static BigInteger ToBigInteger(this byte[] bytes)
        {
            Assert.IsTrue(bytes.Length == 32);

            var value = new BigInteger(bytes.Reverse().ToArray());

            return value;
        }

        public static int ToInt(this byte[] bytes)
        {
            var significant = bytes.TakeLast(4);
            
            var value = BitConverter.ToInt32(significant.Reverse().ToArray());

            return value;
        }
    }
}
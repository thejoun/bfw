using System.Numerics;

namespace Extensions
{
    public static class BigIntegerExtensions
    {
        public static string ToHexString(this BigInteger integer)
        {
            return $"0x{integer.ToString("X")}";
        }
    }
}
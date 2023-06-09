﻿using System.Text;

namespace Helpers
{
    public static class ConversionHelper
    {
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            
            return hex.ToString();
        }
    }
}
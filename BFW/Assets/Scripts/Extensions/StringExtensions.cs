namespace Extensions
{
    public static class StringExtensions
    {
        public static string Head(this string str, int count)
        {
            if (count > str.Length)
            {
                return str;
            }

            return str[..count];
        }
    }
}
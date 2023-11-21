namespace Interfaces
{
    public static class AddressExtensions
    {
        public static string Head(this IAddress address)
        {
            return address.AddressHex.Replace("0x", string.Empty)[..6];
        }
    }
}
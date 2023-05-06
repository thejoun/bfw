namespace Interfaces
{
    public interface IAddress
    {
        public string Address { get; }
    }

    public static class IAddressExtensions
    {
        public static string Head(this IAddress address)
        {
            return address.Address.Replace("0x", string.Empty)[..6];
        }
    }
}
namespace Interfaces
{
    public interface IAccount : IAddress
    {
        public string PrivateKey { get; }
    }
}
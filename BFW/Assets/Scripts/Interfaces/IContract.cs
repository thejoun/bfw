using Sirenix.OdinInspector;

namespace Interfaces
{
    [HideReferenceObjectPicker]
    public interface IContract : IAddress
    {
        public string Abi { get; }
    }
}
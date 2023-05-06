using System.Threading.Tasks;

namespace Interfaces
{
    public interface IEcs
    {
        Task<bool> HasAccess(IAddress who, IContract where);
    }
}
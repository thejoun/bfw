using System.Linq;

namespace Interfaces
{
    public interface IEntityRegistry : IRegistry<IEntity>
    {
        public int GetNewId();
    }
}
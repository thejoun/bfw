using System.Numerics;
using Dtos;
using Interfaces;

namespace Objects
{
    public class EntityAddressFilter : IFilter<ComponentValueSetPayload>
    {
        private BigInteger entity;
        private IAddress address;
        
        public EntityAddressFilter(BigInteger entity, IAddress address)
        {
            this.entity = entity;
            this.address = address;
        }
        
        public bool Accepts(ComponentValueSetPayload value)
        {
            return value.Entity == entity && value.ComponentAddress == address.AddressHex;
        }
    }
}
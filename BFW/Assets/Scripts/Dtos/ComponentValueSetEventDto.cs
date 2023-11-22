using System.Numerics;
using Extensions;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Dtos
{
    [Event("ComponentValueSet")]
    public class ComponentValueSetEventDto : IEventDTO
    {
        [Parameter("uint256", "componentId", 1, true)]
        public BigInteger ComponentId { get; set; }

        [Parameter("address", "component", 2, true)]
        public string ComponentAddress { get; set; }

        [Parameter("uint256", "entity", 3, true)]
        public BigInteger Entity { get; set; }
        
        [Parameter("bytes", "data", 4, false)]
        public byte[] Data { get; set; }

        public override string ToString()
        {
            return $"{ComponentAddress} {Entity.ToString().Head(4)}";
        }
    }
}
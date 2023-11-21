using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace Dtos
{
    [Event("ComponentValueSet")]
    public class ComponentValueSetEventDto : IEventDTO
    {
        [Parameter("uint256", "componentId", 1, true)]
        public BigInteger ComponentId { get; set; }

        [Parameter("address", "component", 2, true)]
        public string Component { get; set; }

        [Parameter("uint256", "entity", 3, true)]
        public BigInteger Entity { get; set; }
        
        [Parameter("bytes", "data", 4, false)]
        public byte[] Data { get; set; }

        private async void Xyz()
        {
            var web3 = new Web3();
            
            var eventHandler = web3.Eth.GetEvent<ComponentValueSetEventDto>("xyz");

            var filter = eventHandler.CreateFilterInput<BigInteger?, string>(null, "abc");
            
            var allTransferEventsForContract = await eventHandler.GetAllChangesAsync(filter);
        }

        public override string ToString()
        {
            return $"{ComponentId} {Component} {Entity} {Data}";
        }
    }
}
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Dtos
{
    [FunctionOutput]
    public class PositionDto : IFunctionOutputDTO 
    {
        [Parameter("int16", "x", 1)]
        public virtual int X { get; set; }
        
        [Parameter("int16", "y", 2)]
        public virtual int Y { get; set; }
    }
}
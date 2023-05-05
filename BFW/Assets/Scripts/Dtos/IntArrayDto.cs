using System.Collections.Generic;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Dtos
{
    [FunctionOutput]
    public class IntArrayDto : IFunctionOutputDTO
    {
        [Parameter("uint32[]")]
        public List<int> Values { get; set; }
    }
}
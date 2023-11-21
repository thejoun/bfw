using System;

namespace Unimorph.Core
{
    public interface ITrigger
    {
        public event Action Triggered;
    }
}
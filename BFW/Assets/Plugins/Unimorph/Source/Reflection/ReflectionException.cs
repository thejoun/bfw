using System;

namespace Unimorph.Reflection
{
    public class ReflectionException : Exception
    {
        public ReflectionException() { }
        public ReflectionException(string message) : base(message) { }
    }
}
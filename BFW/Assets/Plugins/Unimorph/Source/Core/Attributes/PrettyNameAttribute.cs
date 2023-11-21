using System;
using JetBrains.Annotations;

namespace Unimorph.Core
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class)]
    public class PrettyNameAttribute : Attribute
    {
        public string Name { get; set; }

        public PrettyNameAttribute(string name)
        {
            Name = name;
        }
    }
}
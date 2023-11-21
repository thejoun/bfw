using System;

namespace Unimorph.Core
{
    /// <summary>
    /// Marks a member as a target for manual injection.
    /// Should be used on classes in most cases.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class ManualInjectAttribute : Attribute
    {
        
    }
}
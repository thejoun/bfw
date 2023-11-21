using System;

namespace Unimorph.Reflection
{
    /// <summary>
    /// An utility class to simply serialize and deserialize .NET types in a Unity context.
    /// </summary>
    public static class TypeSerializer
    {
        public static string Serialize(Type type)
        {
            return type.FullName;
        }
        
        public static Type Deserialize(string fullName)
        {
            var type = Type.GetType(fullName, false, false);

            if (type != null)
            {
                return type;
            }
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(fullName);

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
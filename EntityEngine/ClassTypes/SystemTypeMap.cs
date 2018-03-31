using System;
using System.Collections.Generic;

namespace EntityEngine.ClassTypes
{
    public static class SystemTypeMap
    {
        private static readonly Dictionary<Type, SystemType> _systemTypes = new Dictionary<Type, SystemType>();

        public static SystemType GetSystemType(Type type)
        {
            if (!_systemTypes.TryGetValue(type, out SystemType result))
            {
                result = new SystemType();
                _systemTypes.Add(type, result);
            }
            return result;
        }
    }
}

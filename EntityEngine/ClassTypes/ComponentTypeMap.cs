using System;
using System.Collections.Generic;

namespace EntityEngine.ClassTypes
{
    public static class ComponentTypeMap
    {
        private static readonly Dictionary<Type, ComponentType> _componentTypes = new Dictionary<Type, ComponentType>();

        public static ComponentType GetComponentType(Type type)
        {
            if (!_componentTypes.TryGetValue(type, out ComponentType result))
            {
                result = new ComponentType();
                _componentTypes.Add(type, result);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;

namespace EntityEngine
{
    /// <summary>
    /// Represents a one to one mapping of <see cref="Type"/> to <see cref="ComponentType"/>.
    /// </summary>
    public static class ComponentTypeMap
    {
        private static Dictionary<Type, ComponentType> _componentTypeMap = new Dictionary<Type, ComponentType>();
        
        /// <summary>
        /// Gets the <see cref="ComponentType"/> associated with the given <see cref="Type"/>.       
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of component.</param>
        /// <returns>A <see cref="ComponentType"/> if one already exists, otherwise a new <see cref="ComponentType"/>.</returns>
        public static ComponentType GetComponentType(Type type)
        {
            ComponentType componentType;
            if (!_componentTypeMap.TryGetValue(type, out componentType))
            {
                componentType = new ComponentType();
                _componentTypeMap.Add(type, componentType);
            }

            return componentType;
        }
    }
}

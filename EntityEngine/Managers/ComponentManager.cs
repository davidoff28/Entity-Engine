using System;
using System.Collections.Generic;

namespace EntityEngine.Managers
{
    /// <summary>
    /// Handles the storage of <see cref="IComponent"/> and provides
    /// functionality to add/get/remove <see cref="IComponent"/>.
    /// </summary>
    public sealed class ComponentManager
    {
        // component database. Maps a component type to 
        // a component array.
        private Dictionary<Type, IComponentArray> _componentDatabase;

        /// <summary>
        /// Initializes a new <see cref="ComponentManager"/>.
        /// </summary>
        public ComponentManager()
        {
            _componentDatabase = new Dictionary<Type, IComponentArray>();
        }

        /// <summary>
        /// Adds a <see cref="IComponent"/> to a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        public void AddComponent<T>(Entity entity, T component)
            where T : IComponent
        {
            Type componentType = typeof(T);
            IComponentArray array = GetComponentArray<T>(true);
            array.Add(entity, component);
        }

        /// <summary>
        /// Removes a <see cref="IComponent"/> from an <see cref="Entity"/>. 
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        public void RemoveComponent<T>(Entity entity)
            where T : IComponent
        {
            IComponentArray array = GetComponentArray<T>(false);
            array.Remove(entity);
        }

        /// <summary>
        /// Removes all components from a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void RemoveAllComponents(Entity entity)
        {
            foreach (var components in _componentDatabase)
            {
                var array = components.Value;
                array.Remove(entity);
            }
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> from an <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An <see cref="IComponent"/>.</returns>
        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            IComponentArray array = GetComponentArray<T>(false);
            return (T)array.GetComponent(entity);
        }

        /// <summary>
        /// Gets a <see cref="ComponentArray{T}"/> of a given
        /// component type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <returns>A <see cref="ComponentArray{T}"/>.</returns>
        public ComponentArray<T> GetComponents<T>() where T : IComponent
        {
            return GetComponentArray<T>(false);
        }

        /// <summary>
        /// Checks to see if an <see cref="Entity"/> has a component.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>True if has component, otherwise false.</returns>
        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            IComponentArray array = GetComponentArray<T>(false);
            return array.Contains(entity);
        }

        /// <summary>
        /// Gets a <see cref="IComponentArray"/> of a component type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="create">Create array if one doesnt exist.</param>
        /// <returns>A <see cref="IComponentArray"/>.</returns>
        private ComponentArray<T> GetComponentArray<T>(bool create) where T : IComponent
        {
            Type componentType = typeof(T);
            IComponentArray array;
            if (!_componentDatabase.TryGetValue(componentType, out array) && create)
            {
                array = new ComponentArray<T>();
                _componentDatabase.Add(componentType, array);
            }

            return (ComponentArray<T>)array;
        }
    }
}

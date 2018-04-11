using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    /// <summary>
    /// Handles the <see cref="IComponent"/> matrix and provides functionality to
    /// add/remove/get <see cref="IComponent"/>.
    /// </summary>
    public class ComponentManager
    {        
        private List<List<IComponent>> _componentsByType;

        /// <summary>
        /// Initializes a new <see cref="ComponentManager"/>.
        /// </summary>
        public ComponentManager()
        {
            _componentsByType = new List<List<IComponent>>();            
        }

        /// <summary>
        /// Adds a new <see cref="IComponent"/> to a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to assign the <see cref="IComponent"/> to.</param>        
        /// <returns>A new <see cref="IComponent"/> of the type provided.</returns>
        public T AddComponent<T>(Entity entity) where T : IComponent
        {
            return (T)AddComponent(entity, typeof(T));
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> associated with a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> associated to the <see cref="IComponent"/>.</param>        
        /// <returns>The specified <see cref="IComponent"/> if exists, otherwise null."/></returns>
        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return (T)GetComponent(entity, typeof(T));
        }

        /// <summary>
        /// Gets a list of <see cref="IComponent"/> of the given type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <returns>A list of <see cref="IComponent"/> if exists, otherwise null.</returns>
        public List<T> GetComponentsOfType<T>() where T : IComponent
        {
            return GetComponentsOfType(typeof(T)) as List<T>;
        }

        /// <summary>
        /// Removes a <see cref="IComponent"/> from a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to remove a <see cref="IComponent"/> from.</param>        
        /// <returns>True if removed, otherwise false.</returns>
        public bool RemoveComponent<T>(Entity entity) where T : IComponent
        {
            return RemoveComponent(entity, typeof(T));
        }

        /// <summary>
        /// Removes all <see cref="IComponent"/> fromm a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to remove all components from.</param>
        public void RemoveAllComponents(Entity entity)
        {
            int typeCount = _componentsByType.Count - 1;
            for (int i = typeCount; i >= 0; i--)
            {
                List<IComponent> components = _componentsByType[i];
                if (entity.Id < components.Count)
                {
                    components.RemoveAt(entity.Id);
                }
            }
        }

        /// <summary>
        /// Adds a new <see cref="IComponent"/> to a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to assign the <see cref="IComponent"/> to.</param>
        /// <param name="componentType">The type of <see cref="IComponent"/>.</param>
        /// <returns></returns>
        private IComponent AddComponent(Entity entity, Type componentType)
        {
            Debug.Assert(entity != null, "entity must not be null!");
            Debug.Assert(componentType != null, "componentType must not be null!");

            var component = (IComponent)Activator.CreateInstance(componentType);
            var type = ComponentTypeMap.GetComponentType(componentType);
            List<IComponent> components;

            if (type.Id >= _componentsByType.Count)
            {
                components = new List<IComponent>();
                _componentsByType.Add(components);
            }
            else
            {
                components = _componentsByType[type.Id];
            }

            if (entity.Id >= components.Count)
            {
                components.Add(component);
            }
            else
            {
                components[entity.Id] = component;
            }

            return component;
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> associated with a given <see cref="Entity"/>.
        /// </summary>       
        /// <param name="entity">The <see cref="Entity"/> associated to the <see cref="IComponent"/>.</param>
        /// <param name="componentType">The type of <see cref="IComponent"/> to retrieve.</param>
        /// <returns>The specified <see cref="IComponent"/> if exists, otherwise null."/></returns>
        private IComponent GetComponent(Entity entity, Type componentType)
        {
            Debug.Assert(entity != null, "entity must not be null!");
            Debug.Assert(componentType != null, "componentType must not be null!");

            var type = ComponentTypeMap.GetComponentType(componentType);
            if (type.Id >= _componentsByType.Count) return null;

            List<IComponent> components = _componentsByType[type.Id];
            if (entity.Id >= _componentsByType.Count) return null;

            return components[entity.Id];
        }
       
        /// <summary>
        /// Removes a <see cref="IComponent"/> from a given <see cref="Entity"/>.
        /// </summary>        
        /// <param name="entity">The <see cref="Entity"/> to remove a <see cref="IComponent"/> from.</param>
        /// <param name="componentType">The type of <see cref="IComponent"/> to remove.</param>
        /// <returns>True if removed, otherwise false.</returns>
        private bool RemoveComponent(Entity entity, Type componentType)
        {
            var type = ComponentTypeMap.GetComponentType(componentType);
            if (type.Id >= _componentsByType.Count) return false;

            List<IComponent> components = _componentsByType[type.Id];
            if (entity.Id >= _componentsByType.Count) return false;

            components.RemoveAt(entity.Id);
            return true;
        }

        /// <summary>
        /// Gets a list of <see cref="IComponent"/> the given type.
        /// </summary>        
        /// <param name="componentType">The type of <see cref="IComponent"/>.</param>
        /// <returns>A list of <see cref="IComponent"/> if exists, otherwise null.</returns>
        private List<IComponent> GetComponentsOfType(Type componentType)
        {
            ComponentType type = ComponentTypeMap.GetComponentType(componentType);
            if (type.Id >= _componentsByType.Count) return null;

            return (type.Id < _componentsByType.Count) ? _componentsByType[type.Id] : null;
        }
    }
}

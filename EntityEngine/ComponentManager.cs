using System.Collections.Generic;
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
        /// <param name="component">The <see cref="IComponent"/> to add.</param>
        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {            
            ComponentType type = ComponentTypeMap.GetComponentType(typeof(T));
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
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> associated with a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> associated to the <see cref="IComponent"/>.</param>
        /// <param name="component">The <see cref="IComponent"/> tp retrieve.</param>
        /// <returns>The specified <see cref="IComponent"/> if exists, otherwise null."/></returns>
        public IComponent GetComponent<T>(Entity entity, T component) where T : IComponent
        {
            ComponentType type = ComponentTypeMap.GetComponentType(typeof(T));
            if (type.Id >= _componentsByType.Count) return null;

            List<IComponent> components = _componentsByType[type.Id];
            if (entity.Id >= components.Count) return null;

            return components[entity.Id];
        }

        /// <summary>
        /// Removes a <see cref="IComponent"/> from a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The <see cref="Entity"/> to remove a <see cref="IComponent"/> from.</param>
        /// <param name="component">The <see cref="IComponent"/> to remove.</param>
        /// <returns>True if removed, otherwise false.</returns>
        public bool RemoveComponent<T>(Entity entity, T component)
        {
            ComponentType type = ComponentTypeMap.GetComponentType(typeof(T));
            if (type.Id >= _componentsByType.Count) return false;

            List<IComponent> components = _componentsByType[type.Id];
            if (entity.Id >= components.Count) return false;

            components.RemoveAt(entity.Id);
            return true;
        }    
        
        /// <summary>
        /// Gets a <see cref="IComponent"/> array of the given type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IComponent"/>.</typeparam>
        /// <returns>A list of <see cref="IComponent"/> if exists, otherwise null.</returns>
        public List<IComponent> GetComponentsOfType<T>() where T : IComponent
        {
            ComponentType type = ComponentTypeMap.GetComponentType(typeof(T));
            if (type.Id >= _componentsByType.Count) return null;

            return _componentsByType[type.Id];
        }

        /// <summary>
        /// Removes all <see cref="IComponent"/> fromm a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to remove all components from.</param>
        public void RemoveAllComponents(Entity entity)
        {
            int typeCount = _componentsByType.Count -1;
            for (int i = typeCount; i >= 0; i--)
            {
                List<IComponent> components = _componentsByType[i];
                if (entity.Id < components.Count)
                {
                    components.RemoveAt(entity.Id);
                }
            }
        }
    }
}

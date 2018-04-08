using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    /// <summary>
    ///  Represents the core context of the engine and provides functionality to create/remove
    ///  <see cref="Entity"/> objects.
    /// </summary>
    public class Core
    {        
        private Dictionary<Type, EntitySystem> _systems;
        private EntityManager _entityManager;
        private ComponentManager _componentManager;

        /// <summary>
        /// Gets the <see cref="EntityManager"/>.
        /// </summary>
        public EntityManager EntityManager
        {
            get => _entityManager;
        }

        /// <summary>
        /// Gets the <see cref="ComponentManager"/>.
        /// </summary>
        public ComponentManager ComponentManager
        {
            get => _componentManager;
        }

        /// <summary>
        /// Initializes a new <see cref="Core"/>.
        /// </summary>
        public Core()
        {
            _systems = new Dictionary<Type, EntitySystem>();
            _entityManager = new EntityManager();
            _componentManager = new ComponentManager();
        }

        /// <summary>
        /// Creates and initializes a new <see cref="Entity"/>.
        /// </summary>
        /// <returns>A new <see cref="Entity"/>.</returns>
        public Entity CreateEntity()
        {
            return _entityManager.CreateEntity();
        }

        /// <summary>
        /// Removes a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to remove.</param>
        public void DestroyEntity(Entity entity)
        {
            _entityManager.DestroyEntity(entity);
        }

        /// <summary>
        /// Adds an <see cref="EntitySystem"/> to the <see cref="Core"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="EntitySystem"/>.</typeparam>
        /// <returns>The type of <see cref="EntitySystem"/>.</returns>
        public T AddSystem<T>() where T : EntitySystem
        {
            Type systemType = typeof(T);
            if (!_systems.TryGetValue(systemType, out EntitySystem system))
            {
                system = Activator.CreateInstance(systemType) as T;
                system.Core = this;
                _systems.Add(systemType, system);
            }

            return (T)system;
        }
    }
}

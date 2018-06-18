using System.Collections.Generic;

using EntityEngine.Managers;

namespace EntityEngine
{
    /// <summary>
    ///  Represents the core context of the engine and provides 
    ///  functionality to create/remove <see cref="Entity"/>, 
    ///  <see cref="IComponent"/> and <see cref="EntitySystem"/>.
    /// </summary>
    public sealed class Context
    {
        private EntityManager _entityManager;
        private ComponentManager _componentManager;
        private SystemManager _systemManager;

        // Destroyed entities are tracked in the
        // context to prevent early removal of components
        // from an entity if destroyed.        
        private List<Entity> _destroyedEntities;

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
        /// Gets the <see cref="SystemManager"/>.
        /// </summary>
        public SystemManager SystemManager
        {
            get => _systemManager;
        }

        /// <summary>
        /// Gets the list of all active <see cref="Entity"/>.
        /// </summary>
        public List<Entity> Entities
        {
            get => _entityManager.Entities;
        }

        /// <summary>
        /// Initializes a new <see cref="Context"/>.
        /// </summary>
        public Context()
        {
            _entityManager = new EntityManager();
            _componentManager = new ComponentManager();
            _systemManager = new SystemManager();
            _destroyedEntities = new List<Entity>();
        }

        /// <summary>
        /// Updates the engine.
        /// </summary>
        /// <param name="deltaTime">The amount of time to update.</param>
        public void Update(float deltaTime = 0)
        {
            // update all added systems.
            _systemManager.UpdateSystems(deltaTime);

            // clean up destroyed entities if any.
            int count = _destroyedEntities.Count;
            bool IsEmpty = count == 0;
            if (!IsEmpty)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    Entity entity = _destroyedEntities[i];

                    _componentManager.RemoveAllComponents(entity);
                    _entityManager.Destroy(entity);
                }
                _destroyedEntities.Clear();
            }
        }

        #region Entity

        /// <summary>
        /// Creates and initializes a new <see cref="Entity"/>.
        /// </summary>
        /// <returns>A new <see cref="Entity"/>.</returns>
        public Entity CreateEntity()
        {
            return _entityManager.Create();
        }

        /// <summary>
        /// Removes a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void DestroyEntity(Entity entity)
        {
            if (!_destroyedEntities.Contains(entity))
            {
                _destroyedEntities.Add(entity);
            }
        }

        /// <summary>
        /// Removes a given <see cref="Entity"/> immediately.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void DestroyNow(Entity entity)
        {
            _entityManager.Destroy(entity);
        }

        #endregion

        #region Components       

        /// <summary>
        /// Adds a new <see cref="IComponentArray"/> to a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of componenent.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        public void AddComponent<T>(Entity entity, T component)
            where T : IComponent
        {
            _componentManager.AddComponent(entity, component);
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> from a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="IComponent"/>.</returns>
        public T GetComponent<T>(Entity entity)
            where T : IComponent
        {
            return _componentManager.GetComponent<T>(entity);
        }

        /// <summary>
        /// Gets a <see cref="ComponentArray{T}"/> of a given <see cref="IComponent"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <returns>The <see cref="ComponentArray{T}"/>.</returns>
        public ComponentArray<T> GetComponents<T>()
            where T : IComponent
        {
            return _componentManager.GetComponents<T>();
        }

        /// <summary>
        /// Removes a <see cref="IComponent"/> from a given <see cref="Entity"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        public void RemoveComponent<T>(Entity entity, T component)
            where T : IComponent
        {
            _componentManager.RemoveComponent<T>(entity);
        }

        /// <summary>
        /// Checks to see if a given <see cref="Entity"/> has a type of
        /// <see cref="IComponent"/>.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>True if entity has component, otherwise false.</returns>
        public bool HasComponent<T>(Entity entity)
            where T : IComponent
        {
            return _componentManager.HasComponent<T>(entity);
        }

        #endregion

        #region Systems

        /// <summary>
        /// Adds an <see cref="EntitySystem"/> to the <see cref="Context"/>.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="EntitySystem"/>.</typeparam>
        /// <returns>The type of <see cref="EntitySystem"/>.</returns>
        public T AddSystem<T>() where T : EntitySystem
        {
            return (T)_systemManager.AddSystem(this, typeof(T));
        }

        /// <summary>
        /// Gets an <see cref="EntitySystem"/> of a given type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="EntitySystem"/>.</typeparam>
        /// <returns>An <see cref="EntitySystem"/> if found, or null.</returns>
        public T GetSystem<T>() where T : EntitySystem
        {
            return (T)_systemManager.GetSystem(typeof(T));
        }

        /// <summary>
        /// Removes an <see cref="EntitySystem"/> of a given type.
        /// </summary>
        /// /// <typeparam name="T">The type of <see cref="EntitySystem"/> to remove.</typeparam>
        public void RemoveSytsem<T>() where T : EntitySystem
        {
            _systemManager.RemoveSytsem(typeof(T));
        }

        #endregion
    }
}

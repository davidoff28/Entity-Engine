using System.Collections.Generic;

namespace EntityEngine.Managers
{
    /// <summary>
    /// Handles the creation and deletion of <see cref="Entity"/>.
    /// </summary>
    public sealed class EntityManager
    {
        private static int _nextId;
        private Stack<int> _idPool;
        private Stack<Entity> _entityPool;
        private List<Entity> _entities;

        /// <summary>
        /// Gets a list of all entities.
        /// </summary>
        public List<Entity> Entities
        {
            get => _entities;
        }

        /// <summary>
        /// Initializes a new <see cref="EntityManager"/>.
        /// </summary>
        public EntityManager()
        {
            _nextId = 0;
            _idPool = new Stack<int>();
            _entityPool = new Stack<Entity>();
            _entities = new List<Entity>();
        }

        /// <summary>
        /// Creates a new <see cref="Entity"/>.
        /// </summary>
        /// <returns>A new <see cref="Entity"/>.</returns>
        public Entity Create()
        {
            // Retrieve an id from id pool if available
            // otherwise provide a new id.
            int id = (_idPool.Count != 0)
                ? _idPool.Pop()
                : _nextId++;

            // Check entity pool is not empty, 
            // create a new entity if it is.
            Entity entity = (_entityPool.Count != 0)
                ? _entityPool.Pop()
                : new Entity(id);

            // add to list of active entities.
            _entities.Add(entity);

            return entity;
        }

        /// <summary>
        /// Destroys a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity to destroy.</param>
        public void Destroy(Entity entity)
        {
            // return if the entity has already
            // been destroyed.
            if (!_entities.Contains(entity))
                return;
            // remove from active entities and reuse id.
            _entities.Remove(entity);

            if (_entityPool.Count < 100)
            {
                _entityPool.Push(entity);
            }
            else
            {
                _idPool.Push(entity.Id);
            }
        }

        /// <summary>
        /// Checks to see if a give <see cref="Entity"/> exists.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Contains(Entity entity)
        {
            return _entities.Contains(entity);
        }
    }
}

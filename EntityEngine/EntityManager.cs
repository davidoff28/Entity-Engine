using System;
using System.Collections.Generic;

namespace EntityEngine
{
    /// <summary>
    /// Handles the creation and destruction of <see cref="Entity"/>.
    /// </summary>
    public class EntityManager
    {
        private static int _nextId;
        private Stack<int> _idPool;
        private List<Entity> _entities;        
                
        /// <summary>
        /// Initializes a new <see cref="EntityManager"/>.
        /// </summary>
        public EntityManager()
        {
            _nextId = 0;
            _idPool = new Stack<int>();
            _entities = new List<Entity>();            
        }

        /// <summary>
        /// Creates a new <see cref="Entity"/>.
        /// </summary>
        /// <returns>The new <see cref="Entity"/>.</returns>
        public Entity CreateEntity()
        {
            int id = (_idPool.Count != 0) ? _idPool.Pop() : _nextId++;
            Entity entity = new Entity(id);

            _entities.Add(entity);            

            return entity;
        }

        /// <summary>
        /// Destroys a given <see cref="Entity"/>. The <see cref="Entity.Id"/> becomes reusable.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to destroy.</param>
        public void DestroyEntity(Entity entity)
        {            
            _entities.Remove(entity);
            _idPool.Push(entity.Id);
        }

        /// <summary>
        /// Checks to see if a given <see cref="Entity"/> is contained within the <see cref="EntityManager"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to find.</param>
        /// <returns>True if <see cref="Entity"/> is found, otherwise false.</returns>
        public bool ContainsEntity(Entity entity)
        {
            return _entities.Contains(entity);
        }
    }
}
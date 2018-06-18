using System;
using System.Collections.Generic;

using EntityEngine.Managers;

namespace EntityEngine
{
    /// <summary>
    /// Represents a sparse array for <see cref="IComponent"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IComponent"/> to store.</typeparam>
    /// <remarks>Simplified version of: 
    /// https://github.com/Spy-Shifty/BrokenBricksECS/blob/master/ECS/Core/ComponentArray.cs </remarks>
    public class ComponentArray<T> : IComponentArray
        where T : IComponent
    {
        // Starting size of the array.
        private int _initialSize;
        // rows.
        private Entity[] _entities;
        // columns.
        private T[] _components;
        // tracks the entity index in the array.
        private Dictionary<Entity, int> _entityIndex;
        // number of non-zero elements,
        private int _size;

        /// <summary>
        /// Gets the element at the given index.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The element at the given index.</returns>
        public T this[int index]
        {
            get => _components[index];
        }
        /// <summary>
        /// Gets the length of the array.
        /// </summary>
        public int Length
        {
            get => _size;
        }

        /// <summary>
        /// Initializes a new <see cref="ComponentArray{T}"/>
        /// with an initial size of 8.
        /// </summary>
        /// <param name="size"></param>
        public ComponentArray(int size = 8)
        {
            _initialSize = size;
            _components = new T[size];
            _entities = new Entity[size];
            _entityIndex = new Dictionary<Entity, int>();
        }

        /// <summary>
        /// Adds an entity with a component to the array.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Add(Entity entity, IComponent component)
        {
            return Add(entity, (T)component);
        }

        /// <summary>
        /// Adds an entity with a component to the array.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Add(Entity entity, T component)
        {
            // check entity exists.
            if (Contains(entity)) return false;

            // resize the array if needed.
            int length = _components.Length;
            if (length == _size)
            {
                ResizeArray(length * 2);
            }

            // set elements.
            _entities[_size] = entity;
            _components[_size] = component;
            _entityIndex.Add(entity, _size);
            // increase size of array.
            _size++;

            return true;
        }

        /// <summary>
        /// Removes a component from a given entity
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool Remove(Entity entity)
        {
            if (!Contains(entity)) return false;

            int lastElement = _size - 1;
            int index = _entityIndex[entity];

            // replace the item to remove with the last element.
            _components[index] = _components[lastElement];
            _entities[index] = _entities[lastElement];

            // update entity map.
            _entityIndex[_entities[index]] = index;
            _entityIndex.Remove(entity);

            // decrease size of matrix.
            _size--;

            // check if the array size can be reduced.
            int newSize = _components.Length / 4;
            if (_size <= newSize && newSize > _initialSize)
            {
                // resize array to half length.
                int length = _components.Length;
                ResizeArray(length / 2);
            }

            return true;
        }

        /// <summary>
        /// Checks to see if an entity exists.
        /// </summary>
        /// <param name="entity">The entity to find.</param>
        /// <returns>True if found, otherwise false.</returns>
        public bool Contains(Entity entity)
        {
            return _entityIndex.ContainsKey(entity);
        }

        /// <summary>
        /// Updates a <see cref="IComponent"/> to a 
        /// given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component.</param>
        public void Update(Entity entity, T component)
        {
            int index = _entityIndex[entity];
            _components[index] = component;
        }

        /// <summary>
        /// Clears the array.
        /// </summary>
        public void Clear()
        {
            _entities = new Entity[_initialSize];
            _components = new T[_initialSize];

            _size = 0;
        }

        /// <summary>
        /// Gets an entity from the array.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The entity.</returns>
        public Entity GetEntity(int index)
        {
            return _entities[index];
        }

        /// <summary>
        /// Gets a <see cref="IComponent"/> from an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An <see cref="IComponent"/>.</returns>
        public IComponent GetComponent(Entity entity)
        {
            return _components[_entityIndex[entity]];
        }

        /// <summary>
        /// Resizes the internal arrays to a given size.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        private void ResizeArray(int newSize)
        {
            // temp arrays.
            var tempEnts = new Entity[newSize];
            var tempComp = new T[newSize];

            // Copy elements to new arrays.
            Array.Copy(_entities, tempEnts, _size);
            Array.Copy(_components, tempComp, _size);

            // set old to new.
            _entities = tempEnts;
            _components = tempComp;
        }
    }
}

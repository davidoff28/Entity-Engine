namespace EntityEngine.Managers
{
    /// <summary>
    /// Represents an interface for component arrays.
    /// </summary>
    internal interface IComponentArray
    {
        /// <summary>
        /// Adds a <see cref="IComponent"/> to an
        /// <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="component">The component."/></param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Add(Entity entity, IComponent component);

        /// <summary>
        /// Removes a <see cref="IComponent"/> from
        /// an <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool Remove(Entity entity);

        /// <summary>
        /// Checks if an <see cref="Entity"/> exists.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if found, otherwise false.</returns>
        bool Contains(Entity entity);

        /// <summary>
        /// Clears all elements.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets an <see cref="Entity"/> from the array.
        /// </summary>
        /// <param name="index">The entity index.</param>
        /// <returns>An entity at the given index.</returns>
        Entity GetEntity(int index);

        /// <summary>
        /// Gets a <see cref="IComponent"/> from
        /// a given <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A component from a given entity.</returns>
        IComponent GetComponent(Entity entity);
    }
}

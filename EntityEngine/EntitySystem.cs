namespace EntityEngine
{
    /// <summary>
    /// An abstract base class, where the <see cref="IComponent"/> data is processed.
    /// </summary>
    public abstract class EntitySystem
    {
        /// <summary>
        /// Gets/Sets the <see cref="Context"/>.
        /// </summary>
        public Context Context
        {
            get;
            internal set;
        }

        /// <summary>
        /// Toggles system On/Off
        /// </summary>
        public bool Toggle
        {
            get;
            set;
        }

        /// <summary>
        /// Updates the <see cref="EntitySystem"/>.
        /// </summary>
        /// <param name="deltaTime">The amount of time to update.</param>
        public abstract void Update(float deltaTime);
    }
}
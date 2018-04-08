namespace EntityEngine
{
    /// <summary>
    /// Represents a <see cref="IComponent"/> type.
    /// </summary>
    public class ComponentType
    {
        private static int _id;
        
        /// <summary>
        /// Gets the id of this <see cref="ComponentType"/>.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ComponentType"/>.
        /// </summary>
        public ComponentType()
        {
            Id = _id++;
        }
    }
}

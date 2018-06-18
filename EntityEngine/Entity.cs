using System;

namespace EntityEngine
{
    /// <summary>
    /// Base class that represents an object.
    /// </summary>
    public sealed class Entity : IEquatable<Entity>
    {
        /// <summary>
        /// The identifier of this <see cref="Entity"/>.
        /// </summary>
        public int Id
        {
            get;
            internal set;
        }

        /// <summary>
        /// Initializes a new <see cref="Entity"/> with an Id.
        /// </summary>
        /// <param name="id">The id.</param>
        internal Entity(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Compares whether two <see cref="Entity"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="Entity"/> instance on the left side of the equal comparison.</param>
        /// <param name="b"><see cref="Entity"/> instance on the right side of the equal comparison.</param>
        /// <returns>True if both instances are equal, otherwise false.</returns>
        public static bool operator ==(Entity a, Entity b)
        {
            return a.Id == b.Id;
        }

        /// <summary>
        /// Compares whether two <see cref="Entity"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="Entity"/> instance on the left side of the not equal comparison.</param>
        /// <param name="b"><see cref="Entity"/> instance on the right side of the not equal comparison.</param>
        /// <returns>True if both instances are not equal, otherwise false.</returns>
        public static bool operator !=(Entity a, Entity b)
        {
            return a.Id != b.Id;
        }

        /// <summary>
        /// Compares whether this <see cref="Entity"/> is equal to another <see cref="Entity"/>.
        /// </summary>
        /// <param name="other">The <see cref="Entity"/> to compare.</param>
        /// <returns>True if both instances are equal, otherwise false.</returns>
        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        /// <summary>
        /// Gets the hashcode of this <see cref="Entity"/> instance.
        /// </summary>
        /// <returns>The hashcode.</returns>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// /Compares whether this <see cref="Entity"/> is equal to another <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns>True if both instances are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Entity) ? (Entity)obj == this : false;
        }

        /// <summary>
        /// Gets a <see cref="String"/> representation of this <see cref="Entity"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Entity: " + Id;
        }
    }
}
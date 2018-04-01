using System;

using EntityEngine.Managers;

namespace EntityEngine
{
    /// <summary>
    /// Base struct that represents a game object.
    /// </summary>
    public struct Entity : IEquatable<Entity>
    {
        private int _id;

        /// <summary>
        /// The identifier of this <see cref="Entity"/>.
        /// </summary>
        public int Id
        {
            get => _id;
        }

        /// <summary>
        /// Initializes a new <see cref="Entity"/> with an Id.
        /// </summary>
        /// <param name="id">The id.</param>
        public Entity(int id)
        {
            _id = id;
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
            return _id == other.Id;
        }

        /// <summary>
        /// Gets the hashcode of this <see cref="Entity"/> instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _id;
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
            return "Entity: " + _id;
        }        
    }


    //public class Entity
    //{
    //    private EntityManager _entityManager;

    //    internal int ComponentBitMask
    //    {
    //        get;
    //        set;
    //    }

    //    internal int SystemBitMask
    //    {
    //        get;
    //        set;
    //    }

    //    public int Id
    //    {
    //        get;
    //        private set;
    //    }

    //    public Entity(EntityManager entityManager, int id)
    //    {
    //        Id = id;
    //        _entityManager = entityManager;
    //    }

    //    public void Destroy()
    //    {
    //        _entityManager.Destroy(this);
    //    }

    //    public T AddComponent<T>() where T : IComponent
    //    {
    //        return (T)_entityManager.AddComponent(this, typeof(T));
    //    }

    //    public T GetComponent<T>() where T : IComponent
    //    {
    //        return (T)_entityManager.GetComponent(this, typeof(T));
    //    }

    //    public void RemoveComponent<T>() where T : IComponent
    //    {
    //        _entityManager.RemoveComponent(this, typeof(T));
    //    }

    //    public void Reset()
    //    {
    //        ComponentBitMask = SystemBitMask = 0;
    //    }
    //}
}

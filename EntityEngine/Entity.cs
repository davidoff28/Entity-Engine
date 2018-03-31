using System;

using EntityEngine.Managers;

namespace EntityEngine
{
    public class Entity
    {
        private EntityManager _entityManager;

        internal int ComponentBitMask
        {
            get;
            set;
        }

        internal int SystemBitMask
        {
            get;
            set;
        }

        public int Id
        {
            get;
            private set;
        }

        public Entity(EntityManager entityManager, int id)
        {
            Id = id;
            _entityManager = entityManager;
        }

        public void Destroy()
        {
            _entityManager.Destroy(this);
        }

        public T AddComponent<T>() where T : Component
        {
            return (T)_entityManager.AddComponent(this, typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_entityManager.GetComponent(this, typeof(T));
        }

        public void RemoveComponent<T>() where T : Component
        {
            _entityManager.RemoveComponent(this, typeof(T));
        }

        public void Reset()
        {
            ComponentBitMask = SystemBitMask = 0;
        }
    }
}

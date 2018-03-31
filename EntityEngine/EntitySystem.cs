using System.Collections.Generic;

namespace EntityEngine
{
    public abstract class EntitySystem
    {
        private bool _isEnabled;
        private Dictionary<int, Entity> _entities;

        internal int BitMask
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get => _isEnabled;
        }

        public Dictionary<int, Entity> Entities
        {
            get => _entities;
        }

        public Filter Filter
        {
            get;
            set;
        }

        public EntitySystem()
        {
            _isEnabled = true;
            Filter = new Filter();
            _entities = new Dictionary<int, Entity>();
            BitMask = 0;
        }

        public EntitySystem(Filter filter)
        {
            Filter = filter;
            _entities = new Dictionary<int, Entity>();
            BitMask = 0;
        }

        public virtual void Update()
        {

        }

        public void Toggle()
        {
            _isEnabled = !_isEnabled;
        }

        internal void AddEntity(Entity entity)
        {
            entity.SystemBitMask |= BitMask;
            if (!_entities.ContainsKey(entity.Id))
            {
                _entities.Add(entity.Id, entity);
            }
        }

        internal void RemoveEntity(Entity entity)
        {
            entity.SystemBitMask &= ~BitMask;
            if (_entities.ContainsKey(entity.Id))
            {
                _entities.Remove(entity.Id);
            }
        }
    }
}

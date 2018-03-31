using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityEngine.Managers;

namespace EntityEngine
{
    public sealed class World
    {
        private EntityManager _entityManager;
        private SystemManager _systemManager;

        public EntityManager EntityManager
        {
            get => _entityManager;
        }

        public SystemManager SystemManager
        {
            get => _systemManager;
        }

        public World()
        {
            _systemManager = new SystemManager(this);
            _entityManager = new EntityManager(this);
        }

        public Entity Create()
        {
            return _entityManager.Create();
        }

        public T AddSystem<T>() where T : EntitySystem
        {
            return _systemManager.AddSystem(typeof(T)) as T;
        }

        public T GetSystem<T>() where T : EntitySystem
        {
            return _systemManager.GetSystem(typeof(T)) as T;
        }

        public void Update()
        {
            _entityManager.Update();
            _systemManager.UpdateSystems();
        }

        internal void NotifyChanges(Entity entity)
        {
            _systemManager.NotifySystems(entity);
        }
    }
}

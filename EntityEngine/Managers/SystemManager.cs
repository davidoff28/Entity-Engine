using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityEngine.ClassTypes;

namespace EntityEngine.Managers
{
    public class SystemManager
    {
        private World _world;
        private Dictionary<int, EntitySystem> _entitySystems;

        public SystemManager(World world)
        {
            _world = world;
            _entitySystems = new Dictionary<int, EntitySystem>();
        }

        public EntitySystem AddSystem(Type type)
        {
            var system = (EntitySystem)Activator.CreateInstance(type);
            var systemType = SystemTypeMap.GetSystemType(type);

            if (!_entitySystems.ContainsKey(systemType.Id))
            {
                system.BitMask |= systemType.Bit;
                _entitySystems.Add(systemType.Id, system);
            }
            else
            {
                system = _entitySystems[systemType.Id];
            }

            return system;
        }

        public EntitySystem GetSystem(Type type)
        {
            var systemType = SystemTypeMap.GetSystemType(type);
            return (_entitySystems.ContainsKey(systemType.Id)) ? _entitySystems[systemType.Id] : null;
        }

        public void NotifySystems(Entity entity)
        {
            for (int i = _entitySystems.Count - 1; i >= 0; i--)
            {
                var system = _entitySystems[i];

                bool containsEntity = (system.BitMask & entity.SystemBitMask) == system.BitMask;
                bool matches = system.Filter.Matches(entity);

                if (matches && !containsEntity)
                {
                    system.AddEntity(entity);
                }
                else if (!matches && containsEntity)
                {
                    system.RemoveEntity(entity);
                }
            }
        }

        public void UpdateSystems()
        {
            for (int i = _entitySystems.Count - 1; i >= 0; i--)
            {
                _entitySystems[i].Update();
            }
        }
    }
}

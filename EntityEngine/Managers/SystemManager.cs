using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityEngine.Managers
{
    /// <summary>
    /// Handles the Updating of all <see cref="EntitySystem"/>.
    /// </summary>
    public sealed class SystemManager
    {
        // Maps a Type to an Processor.
        private Dictionary<Type, EntitySystem> _systems;

        /// <summary>
        /// Initializes a new <see cref="SystemManager"/>.
        /// </summary>
        public SystemManager()
        {
            _systems = new Dictionary<Type, EntitySystem>();
        }

        /// <summary>
        /// Adds a new <see cref="EntitySystem"/> of a given type with a <see cref="Context"/> attached.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> to attach.</param>
        /// <param name="systemType">The type of <see cref="EntitySystem"/>.</param>
        /// <returns>A new <see cref="EntitySystem"/>.</returns>
        public EntitySystem AddSystem(Context context, Type systemType)
        {
            EntitySystem system;
            // check to see if a value exists with the key.
            if (!_systems.TryGetValue(systemType, out system))
            {
                // create a new instance.
                system = (EntitySystem)Activator.CreateInstance(systemType);
                system.Context = context;
                system.Toggle = true;
                _systems.Add(systemType, system);
            }

            return system;
        }

        /// <summary>
        /// Gets an <see cref="EntitySystem"/> of a given type.
        /// </summary>
        /// <param name="systemType">The type of <see cref="EntitySystem"/>.</param>
        /// <returns>An <see cref="EntitySystem"/> if found, or null.</returns>
        public EntitySystem GetSystem(Type systemType)
        {
            return (_systems.ContainsKey(systemType)) ? _systems[systemType] : null;
        }

        /// <summary>
        /// Removes an <see cref="EntitySystem"/> of a given type.
        /// </summary>
        /// <param name="systemType">The type of <see cref="EntitySystem"/>.</param>
        public void RemoveSytsem(Type systemType)
        {
            _systems.Remove(systemType);
        }

        /// <summary>
        /// Updates all stored <see cref="EntitySystem"/>s.
        /// </summary>
        /// <param name="deltaTime">The amount of time to update.</param>
        public void UpdateSystems(float deltaTime)
        {
            // Precheck to see if the dictionary is empty.
            // This prevents uncessary looping.
            if (_systems.Count == 0) return;

            // retrieve all systems.
            EntitySystem[] systems = _systems.Values.ToArray();
            EntitySystem system;

            for (int i = 0; i < systems.Length; i++)
            {
                system = systems[i];
                if (system.Toggle != false)
                    system.Update(deltaTime);
            }
        }
    }
}

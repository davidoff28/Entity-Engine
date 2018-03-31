using System;
using System.Collections.Generic;

using EntityEngine.ClassTypes;

namespace EntityEngine.Managers
{
    public class EntityManager
    {
        private World _world;

        private static int _nextId;
        private Stack<int> _idPool;
        private Stack<Entity> _entityPool;
        private Dictionary<int, Entity> _entities;
        private List<Entity> _destroyedEntities;

        private Dictionary<int, List<Component>> _componentsByType;

        public EntityManager(World world)
        {
            _world = world;
            _nextId = 0;
            _idPool = new Stack<int>();
            _entityPool = new Stack<Entity>();

            _entities = new Dictionary<int, Entity>();
            _destroyedEntities = new List<Entity>();

            _componentsByType = new Dictionary<int, List<Component>>();
        }

        public Entity Create()
        {
            Entity entity;
            if (_entityPool.Count > 0)
            {
                entity = _entityPool.Pop();
            }
            else
            {
                int id = (_idPool.Count > 0) ? _idPool.Pop() : _nextId++;
                entity = new Entity(this, id);
            }
            entity.Reset();

            if (!_entities.ContainsKey(entity.Id))
            {
                _entities.Add(entity.Id, entity);
            }
            else
            {
                _entities[entity.Id] = entity;
            }

            return entity;
        }

        public void Destroy(Entity entity)
        {
            if (!_destroyedEntities.Contains(entity))
            {
                _destroyedEntities.Add(entity);
            }
        }

        public void Update()
        {
            if (_destroyedEntities.Count > 0)
            {
                for (int i = _destroyedEntities.Count - 1; i >= 0; i--)
                {
                    Entity entity = _destroyedEntities[i];
                    RemoveAllComponents(entity);
                    if (_entityPool.Count < 100)
                    {
                        _entityPool.Push(entity);
                    }
                    else
                    {
                        _idPool.Push(entity.Id);
                    }
                    _entities.Remove(entity.Id);
                }

                _destroyedEntities.Clear();
            }
        }

        public Entity GetEntity(Entity entity)
        {
            return (_entities.ContainsKey(entity.Id)) ? _entities[entity.Id] : null;
        }

        public Component AddComponent(Entity entity, Type type)
        {
            var component = (Component)Activator.CreateInstance(type);
            var componentType = ComponentTypeMap.GetComponentType(type);
            List<Component> components = null;

            // check to see if it is a new type of component.
            if (!_componentsByType.ContainsKey(componentType.Id))
            {
                components = new List<Component>();
                _componentsByType.Add(componentType.Id, components);
            }
            else
            {
                components = _componentsByType[componentType.Id];
            }

            // If the entity's id is greater than the current component count, then the entity is a new entity so the list needs to be increased. 
            if (entity.Id >= components.Count)
            {
                for (int i = 0; i < 5; i++)
                {
                    components.Add(null);
                }
            }

            components[entity.Id] = component;
            entity.ComponentBitMask |= componentType.Bit;

            _world.NotifyChanges(entity);

            return component;
        }

        public Component GetComponent(Entity entity, Type type)
        {
            var componentType = ComponentTypeMap.GetComponentType(type);
            List<Component> components = _componentsByType[componentType.Id];

            return (entity.Id <= components.Count) ? components[entity.Id] : null;
        }

        public List<Component> GetComponents(Entity entity)
        {
            List<Component> result = new List<Component>();

            for (int i = 0; i < _componentsByType.Count; i++)
            {
                List<Component> temp = _componentsByType[i];
                if (entity.Id <= temp.Count)
                {
                    Component component = temp[entity.Id];
                    if (component != null)
                    {
                        result.Add(component);
                    }
                }
            }

            return result;
        }

        public void RemoveComponent(Entity entity, Type type)
        {
            var componentType = ComponentTypeMap.GetComponentType(type);
            entity.ComponentBitMask &= ~componentType.Bit;

            List<Component> components = _componentsByType[componentType.Id];
            components[entity.Id] = null;

            _world.NotifyChanges(entity);
        }

        public void RemoveAllComponents(Entity entity)
        {
            entity.ComponentBitMask = 0;
            for (int i = _componentsByType.Count - 1; i >= 0; i--)
            {
                List<Component> components = _componentsByType[i];
                if (entity.Id <= components.Count)
                {
                    components[entity.Id] = null;
                }
            }

            _world.NotifyChanges(entity);
        }
    }
}
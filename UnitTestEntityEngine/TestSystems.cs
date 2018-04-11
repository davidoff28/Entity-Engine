using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntityEngine;

namespace UnitTestEntityEngine
{
    public class FloatSystem : EntitySystem
    {
        List<FloatComponent> components;
        ComponentManager manager;

        public FloatSystem()
        {
            manager = Core.ComponentManager;
            components = manager.GetComponentsOfType<FloatComponent>();
        }

        public void Update()
        {
            int count = components.Count;

            for (int i = 0; i < count; i++)
            {
                FloatComponent component = components[i];
                component.Value += 10;
                components[i] = component;
            }
        }
    }

    public class StringSystem : EntitySystem
    {
        List<StringComponent> components;
        ComponentManager manager;

        public StringSystem()
        {
            manager = Core.ComponentManager;
            components = manager.GetComponentsOfType<StringComponent>();
        }

        public void Update()
        {
            int count = components.Count;

            for (int i = 0; i < count; i++)
            {
                StringComponent component = components[i];
                component.Text = string.Format("Entity {0}", i);
                components[i] = component;
            }
        }
    }

    public class BoolSystem : EntitySystem
    {
        List<BoolComponent> components;
        ComponentManager manager;

        public BoolSystem()
        {
            manager = Core.ComponentManager;
            components = manager.GetComponentsOfType<BoolComponent>();
        }

        public void Update()
        {
            int count = components.Count;

            for (int i = 0; i < count; i++)
            {
                BoolComponent component = components[i];
                component.IsActive = !component.IsActive;
                components[i] = component;
            }
        }
    }
}

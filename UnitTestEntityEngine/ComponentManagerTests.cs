using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntityEngine;

namespace UnitTestEntityEngine
{
    [TestClass]
    public class ComponentManagerTests
    {
        Entity entity;
        FloatComponent floatComponent;
        StringComponent stringComponent;
        BoolComponent boolComponent;

        EntityManager entityManager;
        ComponentManager componentManager;

        [TestMethod]
        public void AddCommponent()
        {
            entityManager = new EntityManager();
            componentManager = new ComponentManager();

            entity = entityManager.CreateEntity();
            floatComponent = new FloatComponent() { Value = 10f };
            componentManager.AddComponent<FloatComponent>(entity, floatComponent);
        }
    }
}

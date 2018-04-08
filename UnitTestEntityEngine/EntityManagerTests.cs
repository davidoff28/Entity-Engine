using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntityEngine;

namespace UnitTestEntityEngine
{
    [TestClass]
    public class EntityManagerTests
    {
        
        [TestMethod]
        public void CreateEntityTest()
        {
            EntityManager entityManager = new EntityManager();
            Entity entity = entityManager.CreateEntity();

            Assert.IsNotNull(entity);
            Assert.AreEqual(1, entity.Id);
        }

        [TestMethod]
        public void DestroyEntityTest()
        {
            EntityManager entityManager = new EntityManager();
            Entity entity = entityManager.CreateEntity();
            entityManager.DestroyEntity(entity);

            Assert.IsFalse(entityManager.ContainsEntity(entity));
        }

        [TestMethod]
        public void TwoEntitiesNotEqualTest()
        {
            EntityManager entityManager = new EntityManager();
            Entity entityA = entityManager.CreateEntity();
            Entity entityB = entityManager.CreateEntity();

            Assert.AreEqual(1, entityA.Id);
            Assert.AreEqual(2, entityB.Id);

            Assert.AreNotEqual(entityA, entityB);
        }

        [TestMethod]
        public void EntityManagerIdReuseTest()
        {
            EntityManager entityManager = new EntityManager();
            Entity entityA = entityManager.CreateEntity();
            entityManager.DestroyEntity(entityA);

            Entity entityB = entityManager.CreateEntity();            

            Assert.AreEqual(entityA, entityB);
        }

        [TestMethod]
        public void SeperateEntityManagerTest()
        {
            EntityManager managerA = new EntityManager();
            EntityManager managerB = new EntityManager();

            Entity entityA = managerA.CreateEntity();

            Assert.IsFalse(managerB.ContainsEntity(entityA));
        }
    }
}

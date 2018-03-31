using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    class ComponentA : Component { public int num; }
    class ComponentB : Component { public int num; }

    class SystemA : EntitySystem
    {
        public SystemA()
            : base(Filter.Accept(typeof(ComponentA)))
        {

        }

        public override void Update()
        {
            ComponentA a;
            foreach (var entity in Entities)
            {
                Entity e = entity.Value;
                a = e.GetComponent<ComponentA>();
                a.num += 10;
                //Console.WriteLine("System A is updating: E({0}, {1}, {2})", e.Id, a.num, e.GetHashCode().ToString());
            }
        }
    }

    class SystemB : EntitySystem
    {
        public SystemB()
            : base(Filter.Accept(typeof(ComponentB)))
        {

        }

        public override void Update()
        {
            ComponentB b;
            foreach (var entity in Entities)
            {
                Entity e = entity.Value;
                b = e.GetComponent<ComponentB>();
                b.num += 5;
                // Console.WriteLine("System B is updating: E({0}, {1})", e.Id, b.num);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimulateGameLoop();
        }

        static int MAX = 1000;
        static void SimulateGameLoop()
        {
            World engine = new World();

            SystemA sa = engine.AddSystem<SystemA>();
            SystemB sb = engine.AddSystem<SystemB>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < MAX; i++)
            {
                Entity entity = engine.Create();

                entity.AddComponent<ComponentA>();

                if (i % 2 == 0)
                    entity.AddComponent<ComponentB>();

                entity.Destroy();

                engine.Update();
            }

            stopwatch.Stop();
            double timeTaken = stopwatch.ElapsedMilliseconds;
            double singleFrame = timeTaken / 1000;

            Console.WriteLine("Time taken: {0}(ms) | {1} single frame", timeTaken, singleFrame);
            Console.Read();
        }
    }
}
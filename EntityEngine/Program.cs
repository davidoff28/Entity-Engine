using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    class TestComponent : IComponent { public float X; }
    class Program
    {
        static void Main(string[] args)
        {
            SimulateGameLoop();                        
        }

        static int MAX = 100;
        static void SimulateGameLoop()
        {
            EntityManager manager = new EntityManager();            

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Entity> entity = new List<Entity>();

            for (int i = 0; i < MAX; i++)
            {
                entity.Add(manager.CreateEntity());                          
                //manager.DestroyEntity(entity);          
            }

            stopwatch.Stop();

            Console.WriteLine("Time taken: {0}(ms)", stopwatch.Elapsed.TotalMilliseconds);
            Console.Read();
        }
    }
}
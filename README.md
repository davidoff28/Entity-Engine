# Entity-Engine
A C# Entity Component System framework for use in game development.
This is still a W.I.P so is currently rough around the edges.

An Entity Component System (ECS) is an architectural design pattern that allows dynamic creation of game objects through the use of components.

# How to use
## Core
The Core class is where the creation/destruction of entities and Adding Systems happens.

	Core core = new Core();

## Entity
An entity is what your game objects within your game are; Tanks, Orcs, Swords, even GUI elements like Buttons, etc.
Entity's are created by using the World. An Entity is simply a wrapper around an integer. The integer is used for identifying an Entity within the framework.

	Entity player = core.CreateEntity();
	core.DestroyEntity(player);

### Component
Components are PODs ("Plain Old Data"). The data within the components is what defines an Entity.
Example of a Component:

	public class Position : IComponent
	{
		// Define your data.
		public float X;
		public float Y;
	}

Components can be attached/removed/retrieved from Entities like so:

	core.ComponentManager.AddComponent<Position>(player);
	core.ComponentManager.GetComponent<Position>(player);
	core.ComponentManager.RemoveComponent<Position>(player);

Entities are limited to only one of each component type, e.g. one Position component is allowed but a Position component and a Rotation component is.

# System
A system, called EntitySystem within the framework, is where the processing of entity data occurs.
An EntitySystem is where you define the behaviour for the entities i.e. the algorithms.

	public class PositionSystem : EntitySystem
	{	
		private List<Position> positionComponents;
		public void Start()
		{
			positionsComponents = Core.ComponentManager.GetComponents<Position>();
		}
		public void Update()
		{		
			for (int i = 0; i < positionComponents.Count; i++)
			{
				Position position = positionComponents[i];
				// Your behaviour code...
			}
		}
	}

# Todo
There is many tasks still to do:
- [ ] Unit Test
- [ ] Speed Test
- [X] Comments
- [X] Tidy up framework

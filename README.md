# Entity-Engine
A C# Entity Component System framework for use in game development.
This is still a W.I.P so is currently rough around the edges.

An Entity Component System (ECS) is an architectural design pattern that allows dynamic creation of game objects through the use of components.

# How to use
## World
The world class is the core of the engine. The world class is where the creation/destruction of entities and Adding/Removing Systems.
An Entity is simply a wrapper around an integer. The integer is used for identifying an Entity within the framework.
'''
World world = new World();
'''
## Entity
An entity is what your game objects within your game are; Tanks, Orcs, Swords, even GUI elements like Buttons, etc.

Entity's are created by using the World.
'''
Entity player = world.CreateEntity();
''' 

### Component
Components are PODs ("Plain Old Data"). The data within the components is what defines an Entity.
Example of a Component:

'''
// Create a component by inheriting the IComponent interface
public class Position : IComponent
{
	// Define your data.
	public float X;
	public float Y;
}
'''

Components can be attached/removed/retrieved from Entities like so:

'''
player.AddComponent<Position>();
player.GetComponent<Position>();
player.RemoveComponent<Position>();
'''

Currently there is a maximum limit of 32 components that can be created. 
Entities are also limited to only one of each component type, e.g. one Position component is allowed but a Position component and a Rotation component is.

# System
A system, called EntitySystem within the framework, is where the processing of entity data occurs.
The systems therefore are the behaviours of the entities.
Systems can filter only entities that have the required components to be used within the system during the update.

'''
public class PositionSystem : EntitySystem
{
	public PositionSystem() : base(Filter.Accept(typeof(Position)))
	{
	}
	
	public override void Update()
	{
		foreach(var entity in this.Entities)
		{
			Position pos = entity.GetComponent<Position>();
			// Your behaviour code
		}
	}
}
'''

#Todo
There is many tasks still to do:
- [ ] Unit Test
- [ ] Speed Test
- [ ] Comments
- [ ] Tidy up framework
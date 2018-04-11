
using EntityEngine;

namespace UnitTestEntityEngine
{
    public struct FloatComponent : IComponent
    {
        public float Value;        
    }

    public struct StringComponent : IComponent
    {
        public string Text;
    }

    public struct BoolComponent : IComponent
    {
        public bool IsActive;
    }    
}

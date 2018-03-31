using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityEngine.ClassTypes;

namespace EntityEngine
{
    public sealed class Filter
    {
        internal int ComponentTypeBits
        {
            get;
            set;
        }

        public Filter()
        {
            ComponentTypeBits = 0;
        }

        public static Filter Accept(params Type[] componentTypes)
        {
            Filter filter = new Filter();
            for (int i = 0; i < componentTypes.Length; i++)
            {
                var type = ComponentTypeMap.GetComponentType(componentTypes[i]);
                filter.ComponentTypeBits |= type.Bit;
            }

            return filter;
        }

        public bool Matches(Entity entity)
        {
            if (!(ComponentTypeBits > 0))
            {
                return false;
            }

            bool result = (ComponentTypeBits & entity.ComponentBitMask) == ComponentTypeBits;

            return result || ComponentTypeBits == 0;
        }
    }
}

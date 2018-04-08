using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityEngine
{
    /// <summary>
    /// Represents a base class for processing <see cref="Entity"/> components. 
    /// </summary>
    public abstract class EntitySystem
    {
        /// <summary>
        /// Gets the <see cref="Core"/>.
        /// </summary>
        public Core Core
        {
            get;
            internal set;
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Argument for bindingsChanged Event. Keeps a string representation of 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class BindingsEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindingsEventArgs"/> class.
        /// </summary>
        /// <param name="bindingStringRepresentation">The binding string representation.</param>
        public BindingsEventArgs (string[][] bindingStringRepresentation)
        {
            bindings = bindingStringRepresentation;
        }
        /// <summary>
        /// The bindings
        /// </summary>
        public string[][] bindings;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class BindingsEventArgs : EventArgs
    {
        public BindingsEventArgs (string[][] bindingStringRepresentation)
        {
            bindings = bindingStringRepresentation;
        }
        public string[][] bindings;
    }
}

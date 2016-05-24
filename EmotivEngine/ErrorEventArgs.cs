using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class ErrorEventArgs : EventArgs
    {
        string errorMessage;
        public string ErrorMessages { get { return errorMessage; } }
        public ErrorEventArgs()
        {
            errorMessage = Texts.ErrorMessages.defaultMessage;
        }

        public ErrorEventArgs(string message)
        {
            errorMessage = message;
        }
    }
}

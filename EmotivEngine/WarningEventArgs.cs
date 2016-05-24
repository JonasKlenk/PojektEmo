using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class WarningEventArgs : EventArgs
    {
        string warningMessage;

        public string WarningMessage { get; }
        public WarningEventArgs()
        {
            warningMessage = Texts.WarningMessages.defaultMessage;
        }

        public WarningEventArgs(string message)
        {
            warningMessage = message;
        }
    }
}

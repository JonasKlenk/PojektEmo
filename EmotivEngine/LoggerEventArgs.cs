using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class LoggerEventArgs : EventArgs
    {
        private string addedLog;
        public LoggerEventArgs(string addedLog)
        {
            this.addedLog = addedLog;
        }

        public string getAddedLog()
        {
            return addedLog;
        }
    }
}

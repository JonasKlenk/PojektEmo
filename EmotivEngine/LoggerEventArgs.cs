using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// event arguments for added log events
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class LoggerEventArgs : EventArgs
    {
        /// <summary>
        /// The added log
        /// </summary>
        private string addedLog;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerEventArgs"/> class.
        /// </summary>
        /// <param name="addedLog">The added log.</param>
        public LoggerEventArgs(string addedLog)
        {
            this.addedLog = addedLog;
        }

        /// <summary>
        /// Gets the added log.
        /// </summary>
        /// <returns></returns>
        public string getAddedLog()
        {
            return addedLog;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Arguemnts for warning event
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class WarningEventArgs : EventArgs
    {
        /// <summary>
        /// The warning message
        /// </summary>
        string warningMessage;

        /// <summary>
        /// Gets the warning message.
        /// </summary>
        /// <value>
        /// The warning message.
        /// </value>
        public string WarningMessage { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WarningEventArgs"/> class.
        /// </summary>
        public WarningEventArgs()
        {
            warningMessage = Texts.WarningMessages.defaultMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WarningEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WarningEventArgs(string message)
        {
            warningMessage = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Event arguments for error events
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// The error message
        /// </summary>
        string errorMessage;
        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <value>
        /// The error messages.
        /// </value>
        public string ErrorMessages { get { return errorMessage; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        public ErrorEventArgs()
        {
            errorMessage = Texts.ErrorMessages.defaultMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ErrorEventArgs(string message)
        {
            errorMessage = message;
        }
    }
}

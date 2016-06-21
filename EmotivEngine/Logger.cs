using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Logger class for logging
    /// </summary>
    class Logger
    {
        /// <summary>
        /// The log
        /// </summary>
        private List<string> log = new List<string>();
        /// <summary>
        /// Occurs when [log added].
        /// </summary>
        public event EventHandler<LoggerEventArgs> logAdded;
        /// <summary>
        /// Log level of a logging entry.
        /// </summary>
        public enum loggingLevel {
            /// <summary>
            /// The debug level
            /// </summary>
            debug,
            /// <summary>
            /// The information level
            /// </summary>
            info,
            /// <summary>
            /// The warning level
            /// </summary>
            warning,
            /// <summary>
            /// The error level
            /// </summary>
            error
        };
        /// <summary>
        /// The current log level
        /// </summary>
        static loggingLevel currentLogLevel = loggingLevel.warning;

        /// <summary>
        /// Adds a log if log level is higher then current log level.
        /// </summary>
        /// <param name="senderName">Name of the sender.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public void addLog(string senderName, string message, loggingLevel level)
        {
            if (level < currentLogLevel)
                return;
            StringBuilder logEntry = new StringBuilder();
            logEntry.Append(DateTime.Now).Append(" - ").Append(senderName).Append(": ").Append(message);
            switch (level)
            {
                case loggingLevel.error:
                    logEntry.Append(" <Error>");
                    break;
                case loggingLevel.warning:
                    logEntry.Append(" <Warning>");
                    break;
                case loggingLevel.info:
                    logEntry.Append(" <Info>");
                    break;
                case loggingLevel.debug:
                    logEntry.Append(" <Debug>");
                    break;
            }
            logEntry.Append(System.Environment.NewLine);
            log.Add(logEntry.ToString());

            EventHandler<LoggerEventArgs> lclLogAdded = logAdded;
            if (lclLogAdded != null)
                lclLogAdded(this, new LoggerEventArgs(logEntry.ToString()));
        }

        /// <summary>
        /// Sets the log level.
        /// </summary>
        /// <param name="l">The l.</param>
        public void setLogLevel(loggingLevel l)
        {
            currentLogLevel = l;
        }

        /// <summary>
        /// Gets the log text.
        /// </summary>
        /// <returns></returns>
        public string getLogText()
        {
            StringBuilder output = new StringBuilder();
            foreach (string s in log)
                output.Append(s);
            return output.ToString();
        }

        /// <summary>
        /// Prints the log.
        /// </summary>
        public void printLog()
        {
            Console.Out.Write(getLogText());
        }
        /// <summary>
        /// Resets the log.
        /// </summary>
        public void resetLog()
        {
            log = new List<string>();
        }
    }
}

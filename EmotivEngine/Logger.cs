using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class Logger
    {
        private List<string> log = new List<string>();
        public event EventHandler<LoggerEventArgs> logAdded;
        public enum loggingLevel { debug, info, warning, error };
        static loggingLevel currentLogLevel = loggingLevel.warning;

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
            logEntry.Append("\n");
            log.Add(logEntry.ToString());

            EventHandler<LoggerEventArgs> lclLogAdded = logAdded;
            if (lclLogAdded != null)
                lclLogAdded(this, new LoggerEventArgs(logEntry.ToString()));
        }

        public void setLogLevel(loggingLevel l)
        {
            currentLogLevel = l;
        }

        public string getLogText()
        {
            StringBuilder output = new StringBuilder();
            foreach (string s in log)
                output.Append(s);
            return output.ToString();
        }

        public void printLog()
        {
            Console.Out.Write(getLogText());
        }
        public void resetLog()
        {
            log = new List<string>();
        }
    }
}

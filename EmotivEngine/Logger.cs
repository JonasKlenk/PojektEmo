using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class Logger
    {
        List<string> log = new List<string>();

        public enum loggingLevel { debug, warning, error };
        loggingLevel currentLogLevel = loggingLevel.warning;

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
                case loggingLevel.debug:
                    logEntry.Append(" <Debug>");
                    break;
            }
            logEntry.Append("\n");
            log.Add(logEntry.ToString());
        }

        override public string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (string s in log)
                output.Append(s);
            return output.ToString();
        }

        public void printLog()
        {
            Console.Out.Write(this.ToString());
        }
    }
}

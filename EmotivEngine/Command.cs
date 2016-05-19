using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class Command
    {
        public Command(int commandId, string commandName, int senderId, double intensity)
        {
            this.commandId = commandId;
            this.senderId = senderId;
            this.commandName = commandName;
            this.intensity = intensity;
        }

        protected int commandId { get; }
        protected string commandName { get; }
        protected int senderId { get; }
        protected double intensity { get; }

        override public string ToString()
        {
            return "Command ID: " + this.commandId + ", Sender ID: " + this.senderId + ", command Name: " + this.commandName + ", intensity: " + this.intensity;
        }

    }
}

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

        private int commandId;
        private string commandName;
        private int senderId;
        private double intensity;
        override public string ToString()
        {
            return "Command ID: " + this.commandId + ", Sender ID: " + this.senderId + ", command Name: " + this.commandName + ", intensity: " + this.intensity;
        }

        public int getCommandId() { return commandId; }
        public int getSenderId() { return senderId; }
        public string getCommandName() { return commandName; }
        public double getIntensity() { return intensity; }

    }
}

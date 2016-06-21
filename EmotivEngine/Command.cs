using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Represents a Command given by a controller
    /// </summary>
    class Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <param name="intensity">The intensity of the command.</param>
        public Command(int commandId, string commandName, int senderId, double intensity)
        {
            this.commandId = commandId;
            this.senderId = senderId;
            this.commandName = commandName;
            this.intensity = intensity;
        }

        /// <summary>
        /// The command identifier
        /// </summary>
        private int commandId;
        /// <summary>
        /// The command name
        /// </summary>
        private string commandName;
        /// <summary>
        /// The sender identifier
        /// </summary>
        private int senderId;
        /// <summary>
        /// The intensity of the command
        /// </summary>
        private double intensity;
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            return "Command ID: " + this.commandId + ", Sender ID: " + this.senderId + ", command Name: " + this.commandName + ", intensity: " + this.intensity;
        }

        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        /// <returns></returns>
        public int getCommandId() { return commandId; }
        /// <summary>
        /// Gets the sender identifier.
        /// </summary>
        /// <returns></returns>
        public int getSenderId() { return senderId; }
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <returns></returns>
        public string getCommandName() { return commandName; }
        /// <summary>
        /// Gets the intensity of the command.
        /// </summary>
        /// <returns></returns>
        public double getIntensity() { return intensity; }

    }
}

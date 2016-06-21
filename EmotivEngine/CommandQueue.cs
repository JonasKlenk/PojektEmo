using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// 
    /// </summary>
    class CommandQueue
    {
        /// <summary>
        /// The number of current commands
        /// </summary>
        private int currentCommands;
        /// <summary>
        /// The maximum number of commands
        /// </summary>
        private int maxCommands;
        /// <summary>
        /// The command queue
        /// </summary>
        private ConcurrentQueue<Command> queue;

        /// <summary>
        /// Occurs when [enqueued command].
        /// </summary>
        event EventHandler enqueuedCommand;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueue"/> class.
        /// </summary>
        /// <param name="maxCommands">The maximum commands.</param>
        public CommandQueue(int maxCommands)
        {
            queue = new ConcurrentQueue<Command>();
            this.maxCommands = maxCommands;
        }

        /// <summary>
        /// Enqueues the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>Boolean true if command was succesfully added to queue</returns>
        public Boolean enqueue(Command c)
        {
            if (currentCommands++ >= maxCommands)
                return false;
            queue.Enqueue(c);
            EventHandler lclEnqueued = enqueuedCommand;
            if (lclEnqueued != null)
                lclEnqueued(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Dequeues a command.
        /// </summary>
        /// <returns><see cref="Command"/></returns>
        public Command dequeue()
        {
            Command returnCommand;
            if (queue.TryDequeue(out returnCommand))
            {
                currentCommands--;
                return returnCommand;
            }

            else
            {
                //Logging Operation required!
                return dequeue();
            }
        }
        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>Boolean true if empty</returns>
        public bool isEmpty()
        {
            Command c;
            queue.TryPeek(out c);
            if (c != null)
                return false;
            return true;
        }
    }
}

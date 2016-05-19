using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class CommandQueue
    {
        private int currentCommands;
        private int maxCommands;
        private ConcurrentQueue<Command> queue;

        event EventHandler enqueuedCommand;
        public CommandQueue(int maxCommands)
        {
            queue = new ConcurrentQueue<Command>();
            this.maxCommands = maxCommands;
        }

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

        public Command dequeue()
        {
            Command returnCommand;
            if (queue.TryDequeue(out returnCommand))
            {
                return returnCommand;
            }
                
            else
            {
                //Logging Operation required!
                return dequeue();
            }
        }
        public bool isEmpty()
        {
            if (queue.TryPeek() != null)
                return false;
            return true;
        }
    }
}

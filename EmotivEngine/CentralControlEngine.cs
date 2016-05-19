using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class CentralControlEngine
    {
        private static CentralControlEngine instance;
        private CentralControlEngine() { }
        private Thread inputHandler;
        private bool acceptInput = false;
        private CommandQueue inputQueue = new CommandQueue(8);

        //Todo halten der Controllable devices
        //halten der Controller
        //halten der Mapper

        //+ jeweils register/unregister Methoden

        public static CentralControlEngine Instance
        {
            get
            {
                if (instance == null)
                    instance = new CentralControlEngine();
                return instance;
            }
        }

        public bool isAcceptingInput()
        {
            return acceptInput;
        }
        public bool addCommand(Command c)
        {
            if(acceptInput)
                return inputQueue.enqueue(c);
            return false;
        }

        public void start()
        {
            if (inputHandler == null)
                inputHandler = new Thread(new ThreadStart(run));
            inputHandler.Start();
            acceptInput = true;  
        }

        public void stop()
        {
            acceptInput = false;
            while (!inputQueue.isEmpty())
                Thread.Sleep(10);
            inputHandler.Abort();
        }

        private void run()
        {
            while (true)
            {
                while (inputQueue.isEmpty())
                    Thread.Sleep(10);
                Command c = inputQueue.dequeue();
                
                //TODO:
                //Id für Zielobjekt finden (über Mapper Objekt
                //Mapping d. Befehle anhand von Controller und Controllable ID -> Mapper ID)

            }

        }
    }
}

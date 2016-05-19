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
        private static CentralControlEngine instance = null;
        private CentralControlEngine() { }
        private Thread inputHandler;
        private bool acceptInput = false;
        private CommandQueue inputQueue = new CommandQueue(8);
        private IControllableDevice controllableDevice;
        private IController controller;

        //Todo halten der Controllable devices
        //halten der Controller
        //halten der Mapper

        //+ jeweils register/unregister Methoden

        public static CentralControlEngine Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                instance = new CentralControlEngine();
                return instance;
            }
        }

        public void setController(IController controller)
        {
            this.controller = controller;
            controller.initialize();
        }

        public void setControllableDevice(IControllableDevice controllableDevice)
        {
            this.controllableDevice = controllableDevice;
        }

        public bool isAcceptingInput()
        {
            return acceptInput;
        }
        public bool addCommand(Command c)
        {
            if (acceptInput)
                return inputQueue.enqueue(c);
            return false;
        }

        public void start()
        {
            controller.setActive();
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
            controller.setDeactive();
        }

        private void run()
        {
            while (true)
            {
                while (inputQueue.isEmpty())
                    Thread.Sleep(1);
                Command c = inputQueue.dequeue();
                ThreadPool.QueueUserWorkItem(new WaitCallback(processCommand), c);
                //TODO:
                //Id für Zielobjekt finden (über Mapper Objekt

            }
        }

        private void processCommand(object c)
        {
            //Mapping
            //Mapping d. Befehle anhand von Controller und Controllable ID -> Mapper ID)
            Console.Out.WriteLine(((Command)c).ToString());
        }
    }
}

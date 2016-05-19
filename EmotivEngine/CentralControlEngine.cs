using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class CentralControlEngine
    {
        //Todo halten der Controllable devices
        //halten der Controller
        //halten der Mapper

        //+ jeweils register/unregister Methoden


        private static CentralControlEngine instance = null;
        private CentralControlEngine() { }
        private Thread inputHandler;
        private int highestControllerId = -1;
        private int highestControllableId = -1;
        private bool acceptInput = false;
        private CommandQueue inputQueue = new CommandQueue(8);
        private List<IControllableDevice> controllableDeviceList;
        private List<IController> controllerList;
        private List<ControllerBinding> controllerDeviceMap = new List<ControllerBinding>();

        //anlegen einer Controller - device - Map Verknüpfung
        public void bindControllerDeviceMap(IController controller, IControllableDevice controllableDevice, Mapping mapping)
        {
            unbindControllerDeviceMap(controller);
            controllerDeviceMap.Add(new ControllerBinding(controller, controllableDevice, mapping));
        }
        
        //Löschen einer Controller - device - Map Verknüpfung
        public void unbindControllerDeviceMap(IController controller)
        {
            controllerDeviceMap.RemoveAll(binding => binding.controller == controller);
        }


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

        public void registerController(IController controller)
        {
            controller.setId(++highestControllerId);
            controllerList.Add(controller);
            controller.initialize();
        }

        public void registerControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.setId(++highestControllableId);
            controllableDeviceList.Add(controllableDevice);
            controllableDevice.initialize();
        }

        public void unregisterController(IController controller)
        {
            controller.setDeactive();
            controllerList.Remove(controller);
        }

        public void unregisterControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.setDeactive();
            controllableDeviceList.Remove(controllableDevice);
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
            foreach(IController c in controllerList)
                    c.setActive();
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
            foreach (IController c in controllerList)
                c.setDeactive();
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
            //TODO: TranlsateCommand

            //Find correct target device and perform action
            //controllerDeviceMap.Find(binding => binding.controller.getId() == ((Command)c).getSenderId()).controllableDevice.performAction(/*translated Command*/);
            Console.Out.WriteLine(((Command)c).ToString());
        }

        //Stellt eine Verknüpfung zwischen COntroller - Device - Mapping dar.
        class ControllerBinding
        {
            public IController controller;
            public IControllableDevice controllableDevice;
            public Mapping mapping;
            public ControllerBinding (IController controller, IControllableDevice controllableDevice, Mapping mapping)
            {
                this.controller = controller;
                this.controllableDevice = controllableDevice;
                this.mapping = mapping;
            }
    }
}

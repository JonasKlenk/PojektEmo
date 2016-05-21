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
        // TODO halten der Controllable devices
        //halten der Controller
        //halten der Mapper

        //+ jeweils register/unregister Methoden

        private Logger logger;
        public event EventHandler loggerUpdated;
        private static CentralControlEngine instance = null;
        private CentralControlEngine() { }
        private Thread inputHandler;
        private int highestControllerId = -1;
        private int highestControllableId = -1;
        private bool acceptInput = false;
        private CommandQueue inputQueue = new CommandQueue(8);
        private List<IControllableDevice> controllableDeviceList = new List<IControllableDevice>();
        private List<IController> controllerList = new List<IController>();
        private List<ControllerBinding> controllerDeviceMap = new List<ControllerBinding>();
        private const string name = "CCE";
        private Logger.loggingLevel loggingLevel = Logger.loggingLevel.debug;

        //anlegen einer Controller - device - Map Verknüpfung
        public void bindControllerDeviceMap(IController controller, IControllableDevice controllableDevice, MapEditor mapping)
        {
            unbindControllerDeviceMap(controller);
            controllerDeviceMap.Add(new ControllerBinding(controller, controllableDevice, mapping));
        }

        public string getLogText()
        {
            return logger.getLogText();
        }

        public void resetLog()
        {
            logger = new Logger();
            initializeLoggerEvents();
        }

        private void initializeLoggerEvents()
        {
            logger.setLogLevel(loggingLevel);
            logger.logAdded += (object o, EventArgs e) =>
            {
                EventHandler lclLoggerUpdated = loggerUpdated;
                if (lclLoggerUpdated != null)
                    lclLoggerUpdated(this, e);
                
            };
        }
        //Löschen einer Controller - device - Map Verknüpfung
        public void unbindControllerDeviceMap(IController controller)
        {
            controllerDeviceMap.RemoveAll(binding => binding.controller == controller);
        }

        public bool isRunning()
        {
            if (inputHandler != null)
                return inputHandler.IsAlive;
            return false;
        }

        public IControllableDevice[] getControllableDevices()
        {
            return controllableDeviceList.ToArray();
        }

        public IController[] getControllers()
        {
            return controllerList.ToArray();
        }

        public static CentralControlEngine Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                instance = new CentralControlEngine();
                instance.resetLog();
                return instance;
            }
        }

        public void registerController(IController controller)
        {
            controller.setId(++highestControllerId);
            controllerList.Add(controller);
            controller.initialize();
            logger.addLog(name, String.Format(Texts.Logging.controllerRegisterd, controller.getType(), controller.getId()), Logger.loggingLevel.info);
        }

        public void registerControllableDevice(IControllableDevice controllableDevice)
        {
            logger.addLog(name, String.Format(Texts.Logging.controllableRegisterd, controllableDevice.getType(), controllableDevice.getId()), Logger.loggingLevel.info);
            controllableDevice.setId(++highestControllableId);
            controllableDeviceList.Add(controllableDevice);
            controllableDevice.initialize();

        }

        public void unregisterController(IController controller)
        {
            controller.setDeactive();
            controllerList.Remove(controller);
            logger.addLog(name, String.Format(Texts.Logging.controllerUnregisterd, controller.getType(), controller.getId()), Logger.loggingLevel.info);
        }

        public void unregisterControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.setDeactive();
            controllableDeviceList.Remove(controllableDevice);
            logger.addLog(name, String.Format(Texts.Logging.controllableUnregisterd, controllableDevice.getType(), controllableDevice.getId()), Logger.loggingLevel.info);
        }

        public bool isAcceptingInput()
        {
            return acceptInput;
        }

        public bool addCommand(Command c)
        {

            if (acceptInput)
                if (inputQueue.enqueue(c))
                    logger.addLog(name, String.Format(Texts.Logging.commandAddedToQueue, c.getCommandName(), c.getCommandId(), c.getSenderId(), c.getIntensity()), Logger.loggingLevel.debug);
            return false;
        }

        public void start()
        {
            foreach (IController c in controllerList)
                c.setActive();
            inputHandler = new Thread(new ThreadStart(run));
            inputHandler.Start();
            acceptInput = true;
            logger.addLog(name, Texts.Logging.engineStarted, Logger.loggingLevel.info); 
        }

        public void stop()
        {
            acceptInput = false;
            while (!inputQueue.isEmpty())
                Thread.Sleep(10);
            inputHandler.Abort();
            foreach (IController c in controllerList)
                c.setDeactive();
            logger.addLog(name, Texts.Logging.engineStoppedByUser, Logger.loggingLevel.info);
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
            public MapEditor mapping;
            public ControllerBinding(IController controller, IControllableDevice controllableDevice, MapEditor mapping)
            {
                this.controller = controller;
                this.controllableDevice = controllableDevice;
                this.mapping = mapping;
            }
        }
    }
}
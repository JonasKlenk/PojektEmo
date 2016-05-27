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
        //Logger Object for central Logging. Logging is fully handled by CCE.
        private Logger logger;
        //Central Control Engine Name, used for Logging Messages
        private const string name = "CCE";

        //EventHandler for informing external Programms about internal changes
        public event EventHandler<LoggerEventArgs> loggerUpdated;
        public event EventHandler<BindingsEventArgs> bindingsChanged;
        public event EventHandler mapsChanged;
        public event EventHandler controllerChanged;
        public event EventHandler controllablesChanged;

        //Singleton instance
        private static CentralControlEngine instance = null;
        private CentralControlEngine() { }

        //Variables for setting internally used ids
        private int highestControllerId = -1;
        private int highestControllableId = -1;

        //Is Engine running (and processing input)
        private bool isRunning = false;

        //Lists for managing Objects
        private List<IControllableDevice> controllableDeviceList = new List<IControllableDevice>();
        private List<IController> controllerList = new List<IController>();
        private List<ControllerBinding> controllerDeviceMap = new List<ControllerBinding>();
        private List<Map> mapList = new List<Map>();
        private List<DeviceCategory> knownCategories = new List<DeviceCategory>();

        //Current Log Level
        private Logger.loggingLevel loggingLevel = Logger.loggingLevel.debug;


        #region log
        public void setLogLevel(Logger.loggingLevel ll)
        {
            if (loggingLevel != ll)
            {
                string oldll = this.loggingLevel.ToString("G");
                this.loggingLevel = ll;
                logger.setLogLevel(ll);
                addLog("Log", String.Format(Texts.Logging.logLevelChanged, oldll, loggingLevel.ToString("G")), Logger.loggingLevel.info);
            }
        }

        public void addLog(string sender, string message, Logger.loggingLevel level)
        {
            logger.addLog(sender, message, level);
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
            logger.logAdded += (object o, LoggerEventArgs e) =>
            {
                EventHandler<LoggerEventArgs> lclLoggerUpdated = loggerUpdated;
                if (lclLoggerUpdated != null)
                    lclLoggerUpdated(this, e);

            };
        }

        #endregion log

        #region categories
        public void registerCategories(ICollection<DeviceCategory> dcc)
        {
            foreach (DeviceCategory dc in dcc)
            {
                knownCategories.Add(dc);
                logger.addLog(name, String.Format(Texts.Logging.categoryRegistered, dc.getCategoryName()), Logger.loggingLevel.info);
            }
        }

        public DeviceCategory findCategoryByName(string name)
        {
            return knownCategories.Find(c => c.getCategoryName() == name);
        }
        #endregion categories

        #region binding
        //Anlegen einer Controller - device - Map Verknüpfung
        public void bindControllerDeviceMap(IController controller, IControllableDevice controllableDevice, Map map)
        {
            unbindControllerDeviceMap(controller);
            controllerDeviceMap.Add(new ControllerBinding(controller, controllableDevice, map));
            EventHandler<BindingsEventArgs> lclBindingsChanged = bindingsChanged;
            if (lclBindingsChanged != null)
            {
                string[][] bindingStringRepresentation = new string[controllerDeviceMap.Count][];
                int i = 0;
                foreach (ControllerBinding cb in controllerDeviceMap)
                    bindingStringRepresentation[i++] = new string[] { cb.controller.Name, cb.controllableDevice.Name, cb.map.name };
                lclBindingsChanged(this, new BindingsEventArgs(bindingStringRepresentation));
            }
        }

        //Löschen einer Controller - device - Map Verknüpfung
        public void unbindControllerDeviceMap(IController controller)
        {
            controllerDeviceMap.RemoveAll(binding => binding.controller == controller);
            EventHandler<BindingsEventArgs> lclBindingsChanged = bindingsChanged;
            if (lclBindingsChanged != null)
            {
                string[][] bindingStringRepresentation = new string[controllerDeviceMap.Count][];
                int i = 0;
                foreach (ControllerBinding cb in controllerDeviceMap)
                    bindingStringRepresentation[i++] = new string[] { cb.controller.ToString(), cb.controllableDevice.ToString(), cb.map.name };

                lclBindingsChanged(this, new BindingsEventArgs(bindingStringRepresentation));
            }
        }

        #endregion binding

        public IControllableDevice[] getControllableDevices()
        {
            return controllableDeviceList.ToArray<IControllableDevice>();
        }

        public IController[] getControllers()
        {
            return controllerList.ToArray<IController>();
        }

        public Map[] getMaps()
        {
            return mapList.ToArray<Map>();
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
            if (controller.initialize())
            {
                controllerList.Add(controller);
                logger.addLog(name, String.Format(Texts.Logging.controllerRegistered, controller.getType(), controller.getId()), Logger.loggingLevel.info);
                controller.Warning += new EventHandler<WarningEventArgs>((sender, e) => addLog(((IController)sender).Name, e.WarningMessage, Logger.loggingLevel.warning));
                controller.Error += new EventHandler<ErrorEventArgs>((sender, e) => addLog(((IController)sender).Name, e.ErrorMessages, Logger.loggingLevel.error));
                controller.Error += new EventHandler<ErrorEventArgs>((sender, e) =>
                {
                    ControllerBinding cb = controllerDeviceMap.Find((binding) => binding.controller == ((IController)sender));
                    cb.stopInputHandler();
                    unbindControllerDeviceMap((IController)sender);
                    unregisterController((IController)sender);

                });
            }
            else
            {
                logger.addLog(name, String.Format(Texts.Logging.controllerRegistrationError, controller.Name), Logger.loggingLevel.error);
            }
        }

        public void registerControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.Id = ++highestControllableId;
            controllableDeviceList.Add(controllableDevice);
            logger.addLog(name, String.Format(Texts.Logging.controllableRegistered, controllableDevice.getCategory(), controllableDevice.Id), Logger.loggingLevel.info);
            controllableDevice.Warning += new EventHandler<WarningEventArgs>((sender, e) => addLog(((IController)sender).Name, e.WarningMessage, Logger.loggingLevel.warning));
            controllableDevice.Error += new EventHandler<ErrorEventArgs>((sender, e) => addLog(((IController)sender).Name, e.ErrorMessages, Logger.loggingLevel.error));
            controllableDevice.Error += new EventHandler<ErrorEventArgs>((sender, e) =>
            {
                List<ControllerBinding> cbl = controllerDeviceMap.FindAll((binding) => binding.controllableDevice == ((IControllableDevice)sender));
                foreach (ControllerBinding cb in cbl)
                {
                    cb.stopInputHandler();
                    unbindControllerDeviceMap(cb.controller);
                }
                unregisterControllableDevice((IControllableDevice)sender);

            });
        }

        public void registerMap(Map map)
        {
            if (map != null)
            {
                mapList.Add(map);
                logger.addLog(name, String.Format(Texts.Logging.mapRegistered, map.name), Logger.loggingLevel.info);
            }
            EventHandler lclMapsChanged = mapsChanged;
            if (lclMapsChanged != null)
            {
                lclMapsChanged(this, new EventArgs());
            }
        }
        public void unregisterMap(Map map)
        {
            mapList.Remove(map);
            logger.addLog(name, String.Format(Texts.Logging.mapUnregistered, map.name), Logger.loggingLevel.info);
            EventHandler lclMapsChanged = mapsChanged;
            if (lclMapsChanged != null)
            {
                lclMapsChanged(this, new EventArgs());
            }
        }

        public void unregisterController(IController controller)
        {
            controller.setDeactive();
            controllerList.Remove(controller);
            logger.addLog(name, String.Format(Texts.Logging.controllerUnregistered, controller.getType(), controller.getId()), Logger.loggingLevel.info);
        }

        public void unregisterControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.setDeactive();
            controllableDeviceList.Remove(controllableDevice);
            logger.addLog(name, String.Format(Texts.Logging.controllableUnregistered, controllableDevice.getCategory(), controllableDevice.Id), Logger.loggingLevel.info);
        }

        public bool getIsRunning()
        {
            return isRunning;
        }

        public bool addCommand(Command c)
        {
            if (isRunning)
            {
                ControllerBinding cb = controllerDeviceMap.Find(binding => binding.controller.getId() == c.getSenderId());
                if (cb != null)
                    if (cb.inputQueue.enqueue(c))
                        logger.addLog(name, String.Format(Texts.Logging.commandAddedToQueue, c.getCommandName(), c.getCommandId(), c.getSenderId(), c.getIntensity()), Logger.loggingLevel.debug);
            }
            return false;
        }

        public void start()
        {
            foreach (ControllerBinding cb in controllerDeviceMap)
                cb.startInputHandler();
            isRunning = true;
            logger.addLog(name, Texts.Logging.engineStarted, Logger.loggingLevel.info);
        }

        public void stop()
        {
            isRunning = false;
            foreach (ControllerBinding c in controllerDeviceMap)
            {
                c.stopInputHandler();
            }
            logger.addLog(name, Texts.Logging.engineStoppedByUser, Logger.loggingLevel.info);
        }

        //Stellt eine Verknüpfung zwischen COntroller - Device - Mapping dar.
        class ControllerBinding
        {
            public IController controller;
            public IControllableDevice controllableDevice;
            public Map map;
            public CommandQueue inputQueue = new CommandQueue(8);
            public Thread inputHandler;

            public ControllerBinding(IController controller, IControllableDevice controllableDevice, Map map)
            {
                this.controller = controller;
                this.controllableDevice = controllableDevice;
                this.map = map;
            }

            private void run()
            {
                while (true)
                {
                    while (inputQueue.isEmpty())
                        Thread.Sleep(1);
                    Command c = inputQueue.dequeue();
                    controllableDevice.performAction(map.translate(c));
                }
            }

            public void startInputHandler()
            {
                controller.setActive();
                controllableDevice.setActive();
                if (inputHandler != null)
                {
                    inputHandler.Resume();
                    return;
                }
                inputHandler = new Thread(new ThreadStart(run));
                inputHandler.Start();
            }

            public void stopInputHandler()
            {
                if (inputHandler != null && inputHandler.IsAlive)
                {
                    while (!inputQueue.isEmpty())
                        Thread.Sleep(10);
                    inputHandler.Suspend();
                    controller.setDeactive();
                    controllableDevice.setDeactive();
                }
            }
        }
    }
}
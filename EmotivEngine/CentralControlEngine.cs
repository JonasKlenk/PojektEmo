﻿using System;
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

        private Logger logger;
        public event EventHandler<LoggerEventArgs> loggerUpdated;
        private static CentralControlEngine instance = null;
        private CentralControlEngine() { }
        private int highestControllerId = -1;
        private int highestControllableId = -1;
        private bool isRunning = false;
        private List<IControllableDevice> controllableDeviceList = new List<IControllableDevice>();
        private List<IController> controllerList = new List<IController>();
        private List<ControllerBinding> controllerDeviceMap = new List<ControllerBinding>();
        private const string name = "CCE";
        private Logger.loggingLevel loggingLevel = Logger.loggingLevel.debug;
        private List<Map> mapList = new List<Map>();
        private List<DeviceCategory> knownCategories = new List<DeviceCategory>();
        public void addLog(string sender, string message, Logger.loggingLevel level)
        {
            logger.addLog(sender, message, level);
        }

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

        //Anlegen einer Controller - device - Map Verknüpfung
        public void bindControllerDeviceMap(IController controller, IControllableDevice controllableDevice, Map map)
        {
            unbindControllerDeviceMap(controller);
            controllerDeviceMap.Add(new ControllerBinding(controller, controllableDevice, map));
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
        //Löschen einer Controller - device - Map Verknüpfung
        public void unbindControllerDeviceMap(IController controller)
        {
            controllerDeviceMap.RemoveAll(binding => binding.controller == controller);
        }

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
                ControllerBinding.cce = instance;
                return instance;
            }
        }

        public void registerController(IController controller)
        {
            controller.setId(++highestControllerId);
            controllerList.Add(controller);
            controller.initialize();
            logger.addLog(name, String.Format(Texts.Logging.controllerRegistered, controller.getType(), controller.getId()), Logger.loggingLevel.info);
        }

        public void registerControllableDevice(IControllableDevice controllableDevice)
        {
            controllableDevice.setId(++highestControllableId);
            controllableDeviceList.Add(controllableDevice);
            controllableDevice.initialize();
            logger.addLog(name, String.Format(Texts.Logging.controllableRegistered, controllableDevice.getType(), controllableDevice.getId()), Logger.loggingLevel.info);
        }

        public void registerMap(Map map)
        {
            mapList.Add(map);
            logger.addLog(name, String.Format(Texts.Logging.mapRegistered, map.name), Logger.loggingLevel.info);
        }
        public void unregisterMap(Map map)
        {
            mapList.Remove(map);
            logger.addLog(name, String.Format(Texts.Logging.mapUnregistered, map.name), Logger.loggingLevel.info);
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
            logger.addLog(name, String.Format(Texts.Logging.controllableUnregistered, controllableDevice.getType(), controllableDevice.getId()), Logger.loggingLevel.info);
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
            foreach (IController c in controllerList)
                c.setActive();
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
                while (!c.inputQueue.isEmpty())
                    Thread.Sleep(10);
                c.inputHandler.Suspend();
            }
            foreach (IController c in controllerList)
                c.setDeactive();
            logger.addLog(name, Texts.Logging.engineStoppedByUser, Logger.loggingLevel.info);
        }

        //Stellt eine Verknüpfung zwischen COntroller - Device - Mapping dar.
        class ControllerBinding
        {
            static public CentralControlEngine cce;
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
                    processCommand(c);
                    //TODO:
                    //Id für Zielobjekt finden (über Mapper Objekt

                }
            }

            private void processCommand(Command c)
            {
                cce.controllerDeviceMap.Find(binding => binding.controller.getId() == ((Command)c).getSenderId()).controllableDevice.performAction(cce.controllerDeviceMap.Find(binding => binding.controller.getId() == ((Command)c).getSenderId()).map.translate(c));
                Console.Out.WriteLine(((Command)c).ToString());
            }

            public void startInputHandler()
            {
                if (inputHandler != null)
                {
                    inputHandler.Resume();
                    return;
                }
                inputHandler = new Thread(new ThreadStart(run));
                inputHandler.Start();
            }
        }
    }
}
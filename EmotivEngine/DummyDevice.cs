using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmotivEngine
{
    class DummyDevice : IControllableDevice
    {
        DeviceCategory c = new DeviceCategory("DummyTimCategory", new string[] { "RechtenArmheben", "LinkenArmHeben", "LinkesBeinHeben", "RechtesBeinHeben", "Umfallen" });
        private int id = 42;
        static private DummyDevice singleInstance = null;
        private CentralControlEngine controlEngine;
        private DummyDevice(CentralControlEngine cce) { this.controlEngine = cce; }
        public void enterFallbackMode()
        {
            throw new NotImplementedException();
        }
        public string[] getActions()
        {
            return c.actionList;
        }
        public int getId()
        {
            return this.id;
        }
        public static IControllableDevice getInstance(CentralControlEngine cce)
        {
            if (singleInstance != null)
                return singleInstance;
            return singleInstance = new DummyDevice(cce);
        }
        public DeviceCategory getType()
        {
            return c;
        }
        public void initialize()
        {
            return;
        }
        public bool isReady()
        {
            throw new NotImplementedException();
        }
        public void performAction(Command action)
        {
            controlEngine.addLog("DummyDevice", String.Format("Received command {0} with id {1}", action.getCommandName(), action.getCommandName()), Logger.loggingLevel.debug);
        }
        public bool setActive()
        {
            throw new NotImplementedException();
        }
        public bool setDeactive()
        {
            throw new NotImplementedException();
        }
        public void setId(int id)
        {
            this.id = id;
        }
    }
}
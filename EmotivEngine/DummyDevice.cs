using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmotivEngine
{
    class DummyDevice : IControllableDevice
    {
        DeviceCategory c = new DeviceCategory("DummyCategory", new string[] { "RechtenArmheben", "LinkenArmHeben", "LinkesBeinHeben", "RechtesBeinHeben", "Umfallen" });
        
        private int id = 42;
        static private DummyDevice singleInstance = null;
        private CentralControlEngine controlEngine;

        public event EventHandler<WarningEventArgs> Warning;
        public event EventHandler<ErrorEventArgs> Error;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                this.id=value;
            }
        }

        public string Name
        {
            get
            {
                return c.getCategoryName() + " (ID: " + Id + ")";
            }
        }

        internal DummyDevice(CentralControlEngine cce) { this.controlEngine = cce; }

        public void enterFallbackMode()
        {
            throw new NotImplementedException();
        }

        public string[] getActions()
        {
            return c.ActionList;
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

        public DeviceCategory getCategory()
        {
            return c;
        }

        public bool isReady()
        {
            //throw new NotImplementedException();
            return true;
        }

        public void performAction(Command action)
        {
            controlEngine.addLog(Name, String.Format(Texts.Logging.receivedCommand, action.getCommandName(), action.getCommandName()), Logger.loggingLevel.debug);
        }

        public void setActive()
        {
            //throw new NotImplementedException();
        }

        public void setDeactive()
        {
            //throw new NotImplementedException();
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }
}
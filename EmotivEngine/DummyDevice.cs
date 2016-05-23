using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class DummyDevice : IControllableDevice
    {
        DeviceKategory c = new DeviceKategory("DummyTimCategory", new string[] { "RechtenArmheben", "LinkenArmHeben", "LinkesBeinHeben", "RechtesBeinHeben", "Umfallen" });
        private int id=42;

        public void enterFallbackMode()
        {
            throw new NotImplementedException();
        }

        public string[] getActions()
        {
            return c.actionList ;
        }

        public int getId()
        {
            return this.id;
        }

        public DeviceKategory getType()
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
            throw new NotImplementedException();
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

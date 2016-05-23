using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class TcpDevice : IControllableDevice
    {
        public void enterFallbackMode()
        {
            throw new NotImplementedException();
        }

        public string[] getActions()
        {
            throw new NotImplementedException();
        }

        public int getId()
        {
            throw new NotImplementedException();
<<<<<<< HEAD
        }

        public DeviceCategory getType()
=======
        }

        public IControllableDevice getInstance(CentralControlEngine cce)
        {
            throw new NotImplementedException();
        }

        public DeviceKategory getType()
>>>>>>> 93ed211e6c5c5b0884bbc846fd64bbb43ca81511
        {
            throw new NotImplementedException();
        }

        public void initialize()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}

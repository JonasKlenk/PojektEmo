using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class Map
    {
        private string controller;
        private string controllableDevice;
        private string creationDateTime;
        private int[] commandMapping;

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping)
        {
            this.controller = contollerType;
            this.controllableDevice = controllableDeviceType;
            this.commandMapping = commandoMapping;
            this.creationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        public void serializeXML()
        {

        }



    }
}

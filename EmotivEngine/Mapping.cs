using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace EmotivEngine
{
    class Mapping
    {
        //private ICollection<IController> availiableControllers;
        //private ICollection<IControllableDevice> availiableControllabelDevices;
        private IController[] availiableControllers;
        private IControllableDevice[] availiableControllabelDevices;
        private IController controller;
        private IControllableDevice device;
        private int[] commandMapping;
        /// <summary>
        /// Construktor mit Gui
        /// </summary>
        /// <param name="availiableControllers"></param>
        /// <param name="availiableControllabelDevices"></param>
        public Mapping(IController[] availiableControllers,IControllableDevice[] availiableControllabelDevices)
        {
            this.availiableControllers = availiableControllers;
            this.availiableControllabelDevices = availiableControllabelDevices;
        }

        public void setActiveController(int i)
        {
            controller = availiableControllers[i];
        }
        public void setActiveDevice(int i)
        {
            device= availiableControllabelDevices[i];
        }


        public void bind(int command, int action)
        {
            if (commandMapping == null)
            {
                commandMapping = new int[getCommandList().Length];
                for (int i = 0; i < commandMapping.Length; i++)
                {
                    commandMapping[i] = -1;
                }
            }
            commandMapping[command] = action;
        }

        public void unbind(int index)
        {
            if (commandMapping == null)
                return;
            commandMapping[index] = -1;
        }

        public string[] getCommandList()
        {
            return controller.getCommands();
        }
        public string[] getActionList()
        {
            return device.getActionTypes();
        }
        public string saveMapping(Stream writeStream)
        {
            Map a = new Map(controller.getType(), device.getType(), commandMapping);
            private XmlWriter writer = XmlWriter.Create(;
            
                return null;
        }


        public static Mapping loadMapping(string path)
        {
            return null;
        }

        public string[] getTextCommandMapping()
        {            
            string[] a = new string[commandMapping.Length];
            
            for (int i = 0; i < commandMapping.Length; i++)
            {
                if (commandMapping[i]!=-1)
                    a[i] = getCommandList()[i] +" mit " + getActionList()[commandMapping[i]];
            }
            return a ;
        }

        internal void saveMapping(Stream stream)
        {
            throw new NotImplementedException();
        }
    }


}



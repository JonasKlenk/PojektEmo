using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string creationDateTime;
        private int[] commandMapping;
        private int i;
        private string[] types;   
        /// <summary>
        /// Construktor mit Gui
        /// </summary>
        /// <param name="availiableControllers"></param>
        /// <param name="availiableControllabelDevices"></param>
        public Mapping(ICollection<IController> availiableControllers, ICollection<IControllableDevice> availiableControllabelDevices)
        {
            this.availiableControllers = availiableControllers.ToArray();
            this.availiableControllabelDevices = availiableControllabelDevices.ToArray();
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
        public string saveMapping()
        {
            Map a = new Map(controller.getType(), device.getType(), commandMapping);
            a.serializeXML();
                return null;
        }


        public static Mapping loadMapping(string path)
        {
            return null;
        }

        internal object getTextcommandMapping()
        {
            //TODO: noch lesbar machen
            return commandMapping;
        }
    }


}



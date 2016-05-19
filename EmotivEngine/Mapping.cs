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
        public Mapping(IController controller, IControllableDevice controllabelDevice)
        {
            this.controller = controller;
            this.device = controllabelDevice;
        }
        private Mapping()
        {

        }
        private IController controller;
        private IControllableDevice device;
        private string controllerType;
        private string controllableDeviceType;
        private string CreationDateTime;
        private int[] commandMapping;


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
        public void serializeXML()
        {

        }
        public string saveMapping()
        {
            return null;
        }
        public static Mapping loadMapping(string path)
        {
            return null;
        }

    }
}

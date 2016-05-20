using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;


namespace EmotivEngine
{
    class Mapping
    {
        private ICollection<IController> availiableControllers;
        private ICollection<IControllableDevice> availiableControllabelDevices;
        private string controllerType;
        private string controllableDeviceType;
        private string CreationDateTime;
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

            this.availiableControllers = availiableControllers;
            this.availiableControllabelDevices = availiableControllabelDevices;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MapperGUI gui = new MapperGUI();
            Application.Run(gui);

            gui.setAvailiableControllers(availiableControllers);
            gui.SetAvailiableControllabelDevices(availiableControllabelDevices);



        }

        

        private Mapping()
        {

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



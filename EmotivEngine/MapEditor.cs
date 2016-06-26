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
    /// <summary>
    /// 
    /// </summary>
    class MapEditor
    {
        //private ICollection<IController> availiableControllers;
        //private ICollection<IControllableDevice> availiableControllabelDevices;
        /// <summary>
        /// The availiable controllers
        /// </summary>
        private IController[] availiableControllers;
        /// <summary>
        /// The availiable controllabel devices
        /// </summary>
        private IControllableDevice[] availiableControllabelDevices;
        /// <summary>
        /// The selected controller
        /// </summary>
        private IController controller;
        /// <summary>
        /// The selected device
        /// </summary>
        private IControllableDevice device;
        /// <summary>
        /// Gets the type of the controller.
        /// </summary>
        /// <value>
        /// The type of the controller.
        /// </value>
        public string controllerType { get; }
        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public string deviceType { get; }
        /// <summary>
        /// Gets or sets the name of the map.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }
        /// <summary>
        /// The bindings
        /// </summary>
        private int[] bindings;
        /// <summary>
        /// The command list of the selected Conroller.
        /// </summary>
        private string[] commandList;
        /// <summary>
        /// The action list of the selected Device.
        /// </summary>
        private string[] actionList;
        /// <summary>
        /// Constructor for the Gui
        /// </summary>
        /// <param name="availiableControllers">The availiable controllers.</param>
        /// <param name="availiableControllabelDevices">The availiable controllabel devices.</param>
        public MapEditor(IController[] availiableControllers, IControllableDevice[] availiableControllabelDevices)
        {
            this.availiableControllers = availiableControllers;
            this.availiableControllabelDevices = availiableControllabelDevices;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEditor"/> class.
        /// </summary>
        /// <param name="a">a.</param>
        private MapEditor(Map a)
        {
            name = a.Name;
            bindings = a.Bindings;
            controllerType = a.ControllerType;
            deviceType = a.ControllableDeviceType;
            commandList = a.CommandList;
            actionList = a.ActionList;

        }

        /// <summary>
        /// Sets the active controller.
        /// </summary>
        /// <param name="i">The i.</param>
        public void setActiveController(int i)
        {
            controller = availiableControllers[i];
            commandList = controller.getCommands();
        }
        /// <summary>
        /// Sets the active device.
        /// </summary>
        /// <param name="i">The i.</param>
        public void setActiveDevice(int i)
        {
            device = availiableControllabelDevices[i];
            actionList = device.getActions();
        }
        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void setName(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// Binds the specified command-action-pair.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="action">The action.</param>
        public void bind(int command, int action)
        {
            if (bindings == null)
            {
                bindings = new int[getCommandList().Length];
                for (int i = 0; i < bindings.Length; i++)
                {
                    bindings[i] = -1;
                }
            }
            bindings[command] = action;
        }
        /// <summary>
        /// Unbinds the specified Binding by index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void unbind(int index)
        {
            if (bindings != null)
                
            bindings[index] = -1;
        }
        /// <summary>
        /// Gets the command list.
        /// </summary>
        /// <returns></returns>
        public string[] getCommandList()
        {
            return commandList;
        }
        /// <summary>
        /// Gets the action list.
        /// </summary>
        /// <returns></returns>
        public string[] getActionList()
        {
            return actionList;
        }
        /// <summary>
        /// Saves the mapping.
        /// </summary>
        /// <param name="writeStream">The write stream.</param>
        /// <returns></returns>
        public Map saveMapping(Stream writeStream)
        {
            Map newMap = new Map(controllerType, deviceType, bindings, name, getCommandList(), getActionList());
            newMap.WriteXml(XmlWriter.Create(writeStream));
            return newMap;
            
        }

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns></returns>
        public static MapEditor loadMap(Map map)
        {
            return new MapEditor(map);
        }

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="readStream">The read stream.</param>
        /// <returns></returns>
        public static MapEditor loadMap(Stream readStream)
        {           
            return new MapEditor(Map.ReadXml(XmlReader.Create(readStream)));
        }
        /// <summary>
        /// Gets the text version of the command-action mapping.
        /// </summary>
        /// <returns></returns>
        public string[][] getTextCommandMapping()
        {
            string[][] textMappings;
            if (bindings != null)
            {
                textMappings = new string[bindings.Length][];

            for (int i = 0; i < this.bindings.Length; i++)
            {
                    textMappings[i] = new string[2];
                    textMappings[i][0] = getCommandList()[i];
                if (bindings[i] != -1)
                        textMappings[i][1] = getActionList()[bindings[i]];
                    else textMappings[i][1] = "";
                }
            }
            else
            {
                textMappings = new string[getCommandList().Length][];
                for (int i = 0; i < textMappings.Length; i++)
                    textMappings[i] = new string[] { getCommandList()[i], "" };
            }
            return textMappings;
        }

        /// <summary>
        /// Deletes the mapping.
        /// </summary>
        /// <param name="xmlMapDir">The XML map dir.</param>
        internal void deleteMapping(string xmlMapDir)
        {
            if (File.Exists(xmlMapDir + name + ".xml"))
                File.Delete(xmlMapDir + name + ".xml");
        }
    }


}



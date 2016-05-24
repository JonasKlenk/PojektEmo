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
    class MapEditor
    {
        //private ICollection<IController> availiableControllers;
        //private ICollection<IControllableDevice> availiableControllabelDevices;
        private IController[] availiableControllers;
        private IControllableDevice[] availiableControllabelDevices;
        private IController controller;
        private IControllableDevice device;
        public string controllerType { get; }
        public string deviceType { get; }
        public string name { get; set; }
        private int[] bindings;
        private string[] commandList;
        private string[] actionList;
        /// <summary>
        /// Construktor für Gui
        /// </summary>
        /// <param name="availiableControllers"></param>
        /// <param name="availiableControllabelDevices"></param>
        public MapEditor(IController[] availiableControllers, IControllableDevice[] availiableControllabelDevices)
        {
            this.availiableControllers = availiableControllers;
            this.availiableControllabelDevices = availiableControllabelDevices;
        }
        private MapEditor(Map a)
        {
            name = a.name;
            bindings = a.bindings;
            controllerType = a.controllerType;
            deviceType = a.controllableDeviceType;
            commandList = a.commandList;
            actionList = a.actionList;

        }

        public void setActiveController(int i)
        {
            controller = availiableControllers[i];
            commandList = controller.getCommands();
        }
        public void setActiveDevice(int i)
        {
            device = availiableControllabelDevices[i];
            actionList = device.getActions();
        }
        public void setName(string name)
        {
            this.name = name;
        }
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
        public void unbind(int index)
        {
            if (bindings != null)
                
            bindings[index] = -1;
        }
        public string[] getCommandList()
        {
            return commandList;
        }
        public string[] getActionList()
        {
            return actionList;
        }
        public Map saveMapping(Stream writeStream)
        {
            Map a = new Map(controllerType, deviceType, bindings, name, getCommandList(), getActionList());

            a.WriteXml(XmlWriter.Create(writeStream));
            
        }

        public static MapEditor loadMap(Map map)
        {
            return new MapEditor(map);
        }

        public static MapEditor loadMap(Stream readStream)
        {           
            return new MapEditor(Map.ReadXml(XmlReader.Create(readStream)));
        }
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
            catch (NullReferenceException)
            {
                System.Windows.Forms.MessageBox.Show(Texts.GUITexts.exception);
                throw new NullReferenceException("OutofBoudns");
            }
            
        }

        internal void deleteMapping(string xmlMapDir)
        {
            if (File.Exists(xmlMapDir + name + ".xml"))
                File.Delete(xmlMapDir + name + ".xml");
        }
    }


}



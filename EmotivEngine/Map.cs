using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EmotivEngine
{
    /// <summary>
    /// The serializable Class to save the Command-Action-Bindings
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("MAP")]
    public class Map
    {
        /// <summary>
        /// Gets or sets the type of the controller.
        /// </summary>
        /// <value>
        /// The type of the controller.
        /// </value>
        [System.Xml.Serialization.XmlElement("controllerType")]
        public string ControllerType { get; set; }
        /// <summary>
        /// Gets or sets the type of the controllable device.
        /// </summary>
        /// <value>
        /// The type of the controllable device.
        /// </value>
        [System.Xml.Serialization.XmlElement("controllableDeviceType")]
        public string ControllableDeviceType { get; set; }
        /// <summary>
        /// The creation date time
        /// </summary>
        [System.Xml.Serialization.XmlElement("creationDateTime")]
        public string CreationDateTime;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the bindings.
        /// </summary>
        /// <value>
        /// The bindings.
        /// </value>
        [System.Xml.Serialization.XmlArray("bindings")]
        [System.Xml.Serialization.XmlArrayItem("binding")]
        public int[] Bindings { get; set; }
        /// <summary>
        /// Gets or sets the command list.
        /// </summary>
        /// <value>
        /// The command list.
        /// </value>
        [System.Xml.Serialization.XmlArray("commandList")]
        [System.Xml.Serialization.XmlArrayItem("command")]
        public string[] CommandList { get; set; }
        /// <summary>
        /// Gets or sets the action list.
        /// </summary>
        /// <value>
        /// The action list.
        /// </value>
        [System.Xml.Serialization.XmlArray("actionList")]
        [System.Xml.Serialization.XmlArrayItem("action")]
        public string[] ActionList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="contollerType">Type of the contoller.</param>
        /// <param name="controllableDeviceType">Type of the controllable device.</param>
        /// <param name="commandoMapping">The commando mapping.</param>
        /// <param name="name">The name of the map.</param>
        /// <param name="commandList">The command list.</param>
        /// <param name="actionList">The action list.</param>
        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping, string name, string[] commandList, string[] actionList)
        {
            this.ControllerType = contollerType;
            this.ControllableDeviceType = controllableDeviceType;
            this.Bindings = commandoMapping;
            this.Name = name;
            this.CommandList = commandList;
            this.ActionList = actionList;

            this.CreationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Map"/> class from being created.
        /// </summary>
        private Map() { }

        /// <summary>
        /// Reads the XML-Map.
        /// </summary>
        /// <param name="inputUri">The input URI.</param>
        /// <returns></returns>
        public static Map ReadXml(string inputUri)
        {
 
            XmlReader reader = XmlReader.Create(inputUri);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            //Todo kurrupte XML wirft Exception
            Map a = (Map)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        /// <summary>
        /// Reads the XML-Map.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static Map ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map a = (Map)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        /// <summary>
        /// Writes the XML-Map.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Map));
            ser.Serialize(writer, this);
            writer.Close();
        }


        /// <summary>
        /// Translates a specific command.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        internal Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.Bindings[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity());
        }

        /// <summary>
        /// Translates a specific command by command-id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int translate(int id)
        { return Bindings[id]; }
    }
}

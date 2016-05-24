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
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("MAP")]
    public class Map
    {
        [System.Xml.Serialization.XmlElement("controllerType")]
        public string controllerType { get; set; }
        [System.Xml.Serialization.XmlElement("controllableDeviceType")]
        public string controllableDeviceType { get; set; }
        [System.Xml.Serialization.XmlElement("creationDateTime")]
        public string creationDateTime;
        [System.Xml.Serialization.XmlElement("name")]
        public string name { get; set; }
        [System.Xml.Serialization.XmlArray("bindings")]
        [System.Xml.Serialization.XmlArrayItem("binding")]
        public int[] bindings { get; set; }
        [System.Xml.Serialization.XmlArray("commandList")]
        [System.Xml.Serialization.XmlArrayItem("command")]
        public string[] commandList { get; set; }
        [System.Xml.Serialization.XmlArray("actionList")]
        [System.Xml.Serialization.XmlArrayItem("action")]
        public string[] actionList { get; set; }

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping, string name, string[] commandList, string[] actionList)
        {
            this.controllerType = contollerType;
            this.controllableDeviceType = controllableDeviceType;
            this.bindings = commandoMapping;
            this.name = name;
            this.commandList = commandList;
            this.actionList = actionList;

            this.creationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        private Map() { }

        public static Map ReadXml(string inputUri)
        {
 
            XmlReader reader = XmlReader.Create(inputUri);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map a = (Map)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public static Map ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map a = (Map)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Map));
            ser.Serialize(writer, this);
        }


        internal Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.bindings[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity());
        }

        public int translate(int id)
        { return bindings[id]; }
    }
}

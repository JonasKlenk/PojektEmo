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
        public string ControllerType { get; set; }
        [System.Xml.Serialization.XmlElement("controllableDeviceType")]
        public string ControllableDeviceType { get; set; }
        [System.Xml.Serialization.XmlElement("creationDateTime")]
        public string CreationDateTime;
        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlArray("bindings")]
        [System.Xml.Serialization.XmlArrayItem("binding")]
        public int[] Bindings { get; set; }
        [System.Xml.Serialization.XmlArray("commandList")]
        [System.Xml.Serialization.XmlArrayItem("command")]
        public string[] CommandList { get; set; }
        [System.Xml.Serialization.XmlArray("actionList")]
        [System.Xml.Serialization.XmlArrayItem("action")]
        public string[] ActionList { get; set; }

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

        private Map() { }

        public static Map ReadXml(string inputUri)
        {
 
            XmlReader reader = XmlReader.Create(inputUri);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            //Todo kurrupte XML wirft Exception
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
            writer.Close();
        }


        internal Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.Bindings[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity());
        }

        public int translate(int id)
        { return Bindings[id]; }
    }
}

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
        public string controllerType { get; }
        [System.Xml.Serialization.XmlElement("controllableDeviceType")]
        public string controllableDeviceType { get; }
        [System.Xml.Serialization.XmlElement("creationDateTime")]
        private string creationDateTime;
        [System.Xml.Serialization.XmlElement("name")]
        public string name { get; }
        [System.Xml.Serialization.XmlElement("bindings")]
        public int[] bindings { get; }
        [System.Xml.Serialization.XmlArray("commandList")]
        public string[] commandList { get; }
        [System.Xml.Serialization.XmlArray("actionList")]
        public string[] actionList { get; }

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

        //Hinzugefügt, weil nötig für XML Serialiserr... 
        //TODO eig. Nicht
        private Map() { }

        public static Map ReadXml(string inputUri)
        {
            /*
            XmlReader reader = XmlReader.Create(inputUri);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            Map a = (Map)serializer.Deserialize(reader);
            reader.Close();
            return a;
        */
            return null;
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
            //TODO kp warum der kack nicht geht... ich bin jetztpennen
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            serializer.Serialize(writer, this);
            writer.Close();
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

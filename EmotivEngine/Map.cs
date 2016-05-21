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
    class Map : IXmlSerializable
    {
        private string controllerType;
        private string controllableDeviceType;
        private string creationDateTime;
        private string name;
        private int[] commandMapping;

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping, string name)
        {
            this.controllerType = contollerType;
            this.controllableDeviceType = controllableDeviceType;
            this.commandMapping = commandoMapping;
            this.name = name;
            this.creationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        public XmlSchema GetSchema()
        {
            //TODO Implement!!!
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "controllerType":
                        controllerType = reader.Value;
                        break;
                    case "controllableDeviceType":
                        controllableDeviceType = reader.Value;
                        break;
                    case "creationDateTime":
                        controllableDeviceType = reader.Value;
                        break;
                    case "Bindings":
                        while (reader.Read())
                        {
                            //TODO reader.count
                            switch (reader.Name)
                            {
                                case "r":
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        //TODO throw unkown Attribute (o.ä.) Exception
                        break;
                }
                reader.MoveToNextAttribute();
            }
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Map");

            xmlWriter.WriteAttributeString("controllerType", controllerType);
            xmlWriter.WriteAttributeString("controllableDeviceType", controllableDeviceType);
            xmlWriter.WriteAttributeString("creationDateTime", creationDateTime);


            xmlWriter.WriteStartElement("Bindings");

            for (int i = 0; i < commandMapping.Length; i++)
            {
                xmlWriter.WriteStartElement("Binding");
                xmlWriter.WriteAttributeString("Command", i.ToString());
                xmlWriter.WriteAttributeString("Action", commandMapping[i].ToString());
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.commandMapping[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity()); }

        public int translate(int id) { return commandMapping[id]; }
    }
}

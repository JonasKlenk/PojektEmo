﻿using System;
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
        private string controller;
        private string controllableDevice;
        private string creationDateTime;
        private int[] commandMapping;

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping)
        {
            this.controller = contollerType;
            this.controllableDevice = controllableDeviceType;
            this.commandMapping = commandoMapping;
            this.creationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "controller":
                        controller = reader.Value;
                        break;
                    case "controllableDevice":
                        controllableDevice = reader.Value;
                        break;
                    case "creationDateTime":
                        controllableDevice = reader.Value;
                        break;
                    case "Bindings":
                        while (reader.Read())
                        {
                            reader.coutn
                            switch (reader.Name)
                            {
                                case "r"
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
                reader.MoveToNextAttribute();
            }
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Map");

            xmlWriter.WriteAttributeString("controller", controller);
            xmlWriter.WriteAttributeString("controllableDevice", controllableDevice);
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
    }
}

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
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("MAP")]
    public class Map
    {
        [System.Xml.Serialization.XmlElement("controllerType")]
        private string controllerType;
        [System.Xml.Serialization.XmlElement("controllableDeviceType")]
        private string controllableDeviceType;
        [System.Xml.Serialization.XmlElement("creationDateTime")]
        private string creationDateTime;
        [System.Xml.Serialization.XmlElement("name")]
        private string name;
        [System.Xml.Serialization.XmlElement("commandMapping")]
        private int[] commandMapping;

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping, string name)
        {
            this.controllerType = contollerType;
            this.controllableDeviceType = controllableDeviceType;
            this.commandMapping = commandoMapping;
            this.name = name;
            this.creationDateTime = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

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
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            serializer.Serialize(writer, this);
            writer.Close();
        }

        internal Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.commandMapping[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity());
        }

        public int translate(int id) { return commandMapping[id]; }
    }
}

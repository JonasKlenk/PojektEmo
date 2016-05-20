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
            //TODO
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}

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
        private int[] commandMapping;

        public Map(string contollerType, string controllableDeviceType, int[] commandoMapping)
        {
            this.controllerType = contollerType;
            this.controllableDeviceType = controllableDeviceType;
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

        public Command translate(Command c)
        {
            //TODO: "dummy" durch richtige Namensfindung ersetzen
            return new Command(this.commandMapping[c.getCommandId()], "dummy", c.getSenderId(), c.getIntensity()); }

        public int translate(int id) { return commandMapping[id]; }
    }
}

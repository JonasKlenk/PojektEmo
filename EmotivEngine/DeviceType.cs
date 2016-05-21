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
    class DeviceKategory : IXmlSerializable
    {
        public DeviceKategory(XmlReader reader)
        {
            ReadXml(reader);
        }

        public DeviceKategory(string Kategory)
        {

        }

        private string KategoryType;
        private string[] ActionList;


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        //TODO Einzelnen Klassen Implementieren
        //in 
    }
}

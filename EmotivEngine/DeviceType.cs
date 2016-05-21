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
    [System.Xml.Serialization.XmlRoot("DEVICETYPE")]
    class DeviceKategory
    {
        public DeviceKategory(string kategory, string[] actionlist)
        {
            this.kategoryType = kategory;
            this.actionList = actionlist;
        }

        [System.Xml.Serialization.XmlElement("KategoryType")]
        private string kategoryType;
        [System.Xml.Serialization.XmlElement("KategoryType")]
        private string[] actionList;

        public static DeviceKategory ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DeviceKategory));
            DeviceKategory a = (DeviceKategory)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            serializer.Serialize(writer, this);
            writer.Close();
        }

        internal string getDeviceKategory()
        {
            return kategoryType;
        }

        //TODO Einzelnen Klassen Implementieren
        //in 
    }
}

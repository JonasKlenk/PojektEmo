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
        public string kategoryType { get; }
        [System.Xml.Serialization.XmlElement("KategoryType")]
        public string[] actionList{ get;  }

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
        //Anmerkung Tim: Was genau in einzelnen Klassen implementieren? Du meinst Umstellung von String literal auf DeviceKategory?
        //DeviceKategory ist eine ziemlich komische Denglische Mischform - DeviceCategory oder GeräteKategorie...
    }
}

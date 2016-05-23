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
    class DeviceCategory
    {
        public DeviceCategory(string categoryName, string[] actionlist)
        {
            this.categoryName = categoryName;
            this.actionList = actionlist;
        }

        [System.Xml.Serialization.XmlElement("CategoryName")]
        public string categoryName { get; }
        //TODO: Zweimal der gleiche Elementname - ich glaube nicht.
        //[System.Xml.Serialization.XmlElement("KategoryType")]
        public string[] actionList{ get;  }

        public static DeviceCategory ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DeviceCategory));
            DeviceCategory a = (DeviceCategory)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));
            serializer.Serialize(writer, this);
            writer.Close();
        }

        internal string getCategoryName()
        {
            return categoryName;
        }

        //TODO Einzelnen Klassen Implementieren
        //in 
        //Anmerkung Tim: Was genau in einzelnen Klassen implementieren? Du meinst Umstellung von String literal auf DeviceKategory?
        //DeviceKategory ist eine ziemlich komische Denglische Mischform - DeviceCategory oder GeräteKategorie...
    }
}

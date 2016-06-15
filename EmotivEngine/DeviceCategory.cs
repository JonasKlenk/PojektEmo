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
    public class DeviceCategory
    {
        [System.Xml.Serialization.XmlElement("CategoryName")]
        public string CategoryName { get; }
        [System.Xml.Serialization.XmlArray("ActionList")]
        [System.Xml.Serialization.XmlArrayItem("Action")] 
        public string[] ActionList{ get;  }
        //public string[] actionList;

        public DeviceCategory(string categoryName, string[] actionlist)
        {
            this.CategoryName = categoryName;
            this.ActionList = actionlist;
        }
        public DeviceCategory() { }

        public static DeviceCategory ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DeviceCategory));
            DeviceCategory a = (DeviceCategory)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DeviceCategory));
            ser.Serialize(writer, this);
            writer.Close();
        }

        public string getCategoryName()
        {
            return CategoryName;
        }

        //TODO Einzelnen Klassen Implementieren
        //in 
        //Anmerkung Tim: Was genau in einzelnen Klassen implementieren? Du meinst Umstellung von String literal auf DeviceKategory?
        //DeviceKategory ist eine ziemlich komische Denglische Mischform - DeviceCategory oder GeräteKategorie...
    }
}

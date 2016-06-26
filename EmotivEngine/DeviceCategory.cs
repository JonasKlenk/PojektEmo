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
    /// <summary>
    /// The Class DeviceCategory
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("DEVICETYPE")]
    public class DeviceCategory
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [System.Xml.Serialization.XmlElement("CategoryName")]
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or sets the list of supported actions.
        /// </summary>
        /// <value>
        /// The action list.
        /// </value>
        [System.Xml.Serialization.XmlArray("ActionList")]
        [System.Xml.Serialization.XmlArrayItem("Action")]
        public string[] ActionList { get; set; }
        //public string[] actionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCategory"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="actionlist">The actionlist.</param>
        public DeviceCategory(string categoryName, string[] actionlist)
        {
            this.CategoryName = categoryName;
            this.ActionList = actionlist;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceCategory"/> class. Parameterless Construktor for the serializer
        /// </summary>
        public DeviceCategory() { }

        /// <summary>
        /// Reads a XML-Map.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static DeviceCategory ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DeviceCategory));
            DeviceCategory a = (DeviceCategory)serializer.Deserialize(reader);
            reader.Close();
            return a;
        }

        /// <summary>
        /// Writes a XML-Map.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DeviceCategory));
            ser.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        /// <returns></returns>
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

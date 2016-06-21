using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emotiv;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace EmotivEngine
{
    class Programme
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Directory.CreateDirectory("./Data");

            //Dronenkategorie anlegen.
            //DeviceCategory a = new DeviceCategory("drone", new string[] { "Neutral", "Throttle Up", "Throttle Down", "Yaw Left", "Yaw Right", "Nick Down", "Nick Up", "Roll Left", "Roll Right" });
            //Directory.CreateDirectory("./Data/Categories/");
            //XmlWriter xmlw = XmlWriter.Create("./Data/Categories/drone.xml");
            //a.WriteXml(xmlw);
            //xmlw.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

        }
    }
}

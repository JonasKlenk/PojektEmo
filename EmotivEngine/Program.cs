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
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //DeviceCategory a = new DeviceCategory("drone", new string[] { "Neutral", "Throttle Up", "Throttle Down", "Yaw Left", "Yaw Right", "Nick Down", "Nick Up", "Roll Left", "Roll Right" });
            //Directory.CreateDirectory("./Data/Categories/");
            //a.WriteXml(XmlWriter.Create("./Data/Categories/drone.xml"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

        }
    }
}

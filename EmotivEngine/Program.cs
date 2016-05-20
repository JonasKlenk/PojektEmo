using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emotiv;
using System.Threading;
using System.Windows.Forms;

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
            CentralControlEngine cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            cce.start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MapperGUI());
        }
    }
}

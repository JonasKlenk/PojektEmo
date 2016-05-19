using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emotiv;
using System.Threading;

namespace EmotivEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            CentralControlEngine cce = CentralControlEngine.Instance;
            cce.registerController(EmoController.getInstance(cce));
            cce.start();
        }
    }
}

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

        enum Command { moveLeft, moveRight, moveElswhere }
        static EmoEngine engine;
        static void Main(string[] args)
        {
            engine = EmoEngine.Instance;

            Console.Out.WriteLine(engine.ToString());
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(connected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(discon);

            //engine.EmoStateUpdated += new EmoEngine.EmoStateUpdatedEventHandler(stateChange)

            engine.RemoteConnect("127.0.0.1", 1726);

            //engine.Connect();
            EmoState ES = new EmoState();

            for (int i = 0; i < 1000; i++)
            {
                engine.ProcessEvents();
                Thread.Sleep(100);
            }
        }

        static void connected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("connected");
        }

        static void discon(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("disc");
        }

        static void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("User Added Event has occured");

            Profile p = engine.GetUserProfile((uint)e.userId);
            Console.Out.WriteLine("UserID = " + e.userId);
            Console.Out.WriteLine(p.ToString());
            
        }

        static void stateChange(object sender, EmoEngineEventArgs e)
        {
            
           // Console.WriteLine(ES.GetWirelessSignalStatus());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emotiv;
using EmotivEngine;
using System.Threading;

namespace EmotivEngine
{
    class EmoController : IController
    {
        public event EventHandler Error;
        public event EventHandler Warning;

        private CentralControlEngine controlEngine;
        private EmoState lastEmoState = new EmoState();
        private Thread runEngineThread;
        private EmoEngine engine = null;
        static private EmoController singleInstance = null;

        private int id;

        public int getId()
        {
            return id;
        }

        private enum command : int
        {
            CognitivePush = 0, CognitivePull = 1, CognitiveLift = 2, CognitiveDrop = 3, CognitiveLeft = 4,
            CognitiveRight = 5, CognitiveRotateLeft = 6, CognitiveRotateRight = 7, CognitiveRotateClockwise = 8,
            CognitiveRotateCounterClockwise = 9, CognitiveRotateForward = 10, CognitiveRotateBackwards = 11,
            ExpressiveSmile = 12, ExpressiveLookLeft = 13, ExpressiveLookRight = 14, ExpressiveWinkLeft = 15, ExpressiveWinkRight = 16, ExpressiveBlink = 17
        };

        private EmoController(CentralControlEngine cce) { this.controlEngine = cce; }


        public static IController getInstance(CentralControlEngine cce)
        {
            if (singleInstance != null)
                return singleInstance;
            return singleInstance = new EmoController(cce);

        }

        public string[] getCommands()
        {
            return (String[])Enum.GetValues(typeof(command));
        }

        public bool initialize()
        {
            engine = EmoEngine.Instance;
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(connected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(discon);
            engine.AffectivEmoStateUpdated += Engine_AffectivEmoStateUpdated;
            engine.CognitivEmoStateUpdated += Engine_CognitivEmoStateUpdated;
            engine.EmoEngineEmoStateUpdated += Engine_EmoEngineEmoStateUpdated;
            engine.ExpressivEmoStateUpdated += Engine_ExpressivEmoStateUpdated;

            engine.RemoteConnect("127.0.0.1", 1726);
            return true;
        }

        private void Engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            bool success = true;
            switch (e.emoState.CognitivGetCurrentAction())
            {
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    controlEngine.addCommand(new Command((int)command.CognitivePush, command.CognitivePush.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    success = controlEngine.addCommand(new Command((int)command.CognitivePull, command.CognitivePull.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveLift, command.CognitiveLift.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveDrop, command.CognitiveDrop.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveLeft, command.CognitiveLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRight, command.CognitiveRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_LEFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateLeft, command.CognitiveRotateLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_RIGHT:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateRight, command.CognitiveRotateRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_CLOCKWISE:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateClockwise, command.CognitiveRotateClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_COUNTER_CLOCKWISE:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateCounterClockwise, command.CognitiveRotateCounterClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_FORWARDS:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateForward, command.CognitiveRotateForward.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_REVERSE:
                    success = controlEngine.addCommand(new Command((int)command.CognitiveRotateBackwards, command.CognitiveRotateBackwards.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
            }
            if (!success)
            {
                EventHandler lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, e);
            }
        }

        private void Engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.BAD_SIGNAL)
            {
                EventHandler lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, e);
            }
            else if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.NO_SIGNAL)
            {
                EventHandler lclError = Error;
                if (lclError != null)
                    lclError(this, e);
            }
        }

        private void Engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            double smileThreshold = 0.5;

            bool success = true;
            if ((double)e.emoState.ExpressivGetSmileExtent() > smileThreshold)
                success = controlEngine.addCommand(new Command((int)command.ExpressiveSmile, command.ExpressiveSmile.ToString("G"), this.id, e.emoState.ExpressivGetSmileExtent()));
            if (e.emoState.ExpressivIsLookingLeft())
                success = controlEngine.addCommand(new Command((int)command.ExpressiveLookLeft, command.ExpressiveLookLeft.ToString("G"), this.id, 1.0));
            if (e.emoState.ExpressivIsLookingRight())
                success = controlEngine.addCommand(new Command((int)command.ExpressiveLookRight, command.ExpressiveLookRight.ToString("G"), this.id, 1.0));
            if (e.emoState.ExpressivIsLeftWink())
                success = controlEngine.addCommand(new Command((int)command.ExpressiveWinkLeft, command.ExpressiveWinkLeft.ToString("G"), this.id, 1.0));
            if (e.emoState.ExpressivIsRightWink())
                success = controlEngine.addCommand(new Command((int)command.ExpressiveWinkRight, command.ExpressiveWinkRight.ToString("G"), this.id, 1.0));
            if (e.emoState.ExpressivIsBlink())
                success = controlEngine.addCommand(new Command((int)command.ExpressiveBlink, command.ExpressiveBlink.ToString("G"), this.id, 1.0));

            if (!success)
            {
                EventHandler lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, e);
            }
        }

        private void Engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            bool success = true;
            if (!success)
            {
                EventHandler lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, e);
            }
        }

        public bool isReady()
        {
            return runEngineThread.IsAlive;
        }

        public bool setActive()
        {
            if (runEngineThread == null)
                runEngineThread = new Thread(new ThreadStart(runEngine));
            runEngineThread.Start();
            return true;


        }

        public bool setDeactive()
        {
            runEngineThread.Abort();
            return true;
        }

        private void runEngine()
        {
            while (true)
            {
                engine.ProcessEvents();
                Thread.Sleep(10);
            }
        }


        private void connected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("connected");
        }

        private void discon(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("disc");
        }

        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("User Added Event has occured");

            Profile p = engine.GetUserProfile((uint)e.userId);
            Console.Out.WriteLine("UserID = " + e.userId);
            Console.Out.WriteLine(p.ToString());

        }

        public string getType()
        {
            throw new NotImplementedException();
        }
    }
}

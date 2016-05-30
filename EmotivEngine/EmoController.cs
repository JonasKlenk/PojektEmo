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
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<WarningEventArgs> Warning;
        private static string type = Texts.ControllerTypes.CT_EmotivEPOC;
        private CentralControlEngine controlEngine;
        private EmoState lastEmoState = new EmoState();
        private Thread runEngineThread;
        private EmoEngine engine = null;
        static private EmoController singleInstance = null;

        private int id;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Type {
            get
            {
                return type;
            }
        }
        public string Name
        {
            get
            {
                return type + " (ID: " + id + ")";
            }
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
            return (string[])Enum.GetNames(typeof(command));
        }

        public bool initialize()
        {
            engine = EmoEngine.Instance;
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(connected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(discon);

            try {
                engine.RemoteConnect("127.0.0.1", 1726);
            }
            catch(Emotiv.EmoEngineException e)
            {
                controlEngine.addLog("EmoControler", e.Message, Logger.loggingLevel.error);
                return false;
            }
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
                default:
                    success = true;
                    controlEngine.addLog(Name, String.Format(Texts.Logging.unhandledCommand, e.emoState.CognitivGetCurrentAction()), Logger.loggingLevel.warning);
                    break;
            }
            if (!success)
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, e.emoState.CognitivGetCurrentAction().ToString())));
            }
        }

        private void Engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.BAD_SIGNAL)
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(Texts.WarningMessages.badSignal));
            }
            else if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.NO_SIGNAL)
            {
                EventHandler<ErrorEventArgs> lclError = Error;
                if (lclError != null)
                    lclError(this, new ErrorEventArgs(Texts.ErrorMessages.noSignal));
            }
        }

        private void Engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            double smileThreshold = 0.5;
            if (e.emoState.ExpressivGetSmileExtent() != lastEmoState.ExpressivGetSmileExtent() && (double)e.emoState.ExpressivGetSmileExtent() > smileThreshold)
                if(!controlEngine.addCommand(new Command((int)command.ExpressiveSmile, command.ExpressiveSmile.ToString("G"), this.id, e.emoState.ExpressivGetSmileExtent())))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveSmile.ToString("G"))));
                }
            if (e.emoState.ExpressivIsLookingLeft() != lastEmoState.ExpressivIsLookingLeft())
                if(!controlEngine.addCommand(new Command((int)command.ExpressiveLookLeft, command.ExpressiveLookLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveLookLeft.ToString("G"))));
                }
            if (e.emoState.ExpressivIsLookingRight() != lastEmoState.ExpressivIsLookingRight())
                if(!controlEngine.addCommand(new Command((int)command.ExpressiveLookRight, command.ExpressiveLookRight.ToString("G"), this.id, 1.0)))
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveLookRight.ToString("G"))));
            }
            if (e.emoState.ExpressivIsLeftWink() != lastEmoState.ExpressivIsLeftWink())
                if(!controlEngine.addCommand(new Command((int)command.ExpressiveWinkLeft, command.ExpressiveWinkLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveWinkLeft.ToString("G"))));
                }
            if (e.emoState.ExpressivIsRightWink() != lastEmoState.ExpressivIsRightWink())
                if (!controlEngine.addCommand(new Command((int)command.ExpressiveWinkRight, command.ExpressiveWinkRight.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveWinkRight.ToString("G"))));
                }

            if (e.emoState.ExpressivIsBlink() != lastEmoState.ExpressivIsBlink())
                if(!controlEngine.addCommand(new Command((int)command.ExpressiveBlink, command.ExpressiveBlink.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressiveBlink.ToString("G"))));
                }
        }

        private void Engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            bool success = true;
            if (!success)
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(Texts.WarningMessages.couldntAddcommand));
            }
        }

        public bool isReady()
        {
            return true;        }

        public void setActive()
        {
            engine.AffectivEmoStateUpdated += Engine_AffectivEmoStateUpdated;
            engine.CognitivEmoStateUpdated += Engine_CognitivEmoStateUpdated;
            engine.EmoEngineEmoStateUpdated += Engine_EmoEngineEmoStateUpdated;
            engine.ExpressivEmoStateUpdated += Engine_ExpressivEmoStateUpdated;
            runEngineThread = new Thread(new ThreadStart(runEngine));
            runEngineThread.Start();


        }

        public void setDeactive()
        {
            engine.AffectivEmoStateUpdated -= Engine_AffectivEmoStateUpdated;
            engine.CognitivEmoStateUpdated -= Engine_CognitivEmoStateUpdated;
            engine.EmoEngineEmoStateUpdated -= Engine_EmoEngineEmoStateUpdated;
            engine.ExpressivEmoStateUpdated -= Engine_ExpressivEmoStateUpdated;
            runEngineThread.Abort();
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
            controlEngine.addLog(this.Name, String.Format(Texts.Logging.connected, "engine"), Logger.loggingLevel.info);
        }

        private void discon(object sender, EmoEngineEventArgs e)
        {
            controlEngine.addLog(this.Name, String.Format(Texts.Logging.disconnected, "engine"), Logger.loggingLevel.info);
        }

        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            controlEngine.addLog(this.Name, String.Format(Texts.Logging.emotivUserAdded, e.userId), Logger.loggingLevel.debug);
        }
    }

    //class EmoExpressiveState
    //{

    //    private double smileExtent;
    //    public double SmileExtent { get; }

    //    private bool isLookingLeft;
    //    public bool IsLookingLeft { get; }
    //    private bool isLookingRight;
    //    public bool IsLookingRight { get; }
    //    private bool isLeftWink;
    //    public bool IsLeftWink { get; }
    //    private bool isRightWink;
    //    public bool IsRightWink { get; }
    //    private bool isBlink;
    //    private bool IsBlink { get; }

    //    public EmoExpressiveState(double smileExtent, bool isLookingLeft, bool isLookingRight, bool isLeftWink, bool isRightWink, bool isBlink)
    //    {
    //        this.smileExtent = smileExtent;
    //        this.isLookingLeft = isLookingLeft;
    //        this.isLookingRight = isLookingRight;
    //        this.isLeftWink = isLeftWink;
    //        this.isRightWink = isRightWink;
    //        this.isBlink = isBlink;
    //    }
    //}

}

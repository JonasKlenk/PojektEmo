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
            CognitivPush = 0, CognitivPull = 1, CognitivLift = 2, CognitivDrop = 3, CognitivLeft = 4,
            CognitivRight = 5, CognitivRotateLeft = 6, CognitivRotateRight = 7, CognitivRotateClockwise = 8,
            CognitivRotateCounterClockwise = 9, CognitivRotateForward = 10, CognitivRotateBackwards = 11,
            ExpressivSmile = 12, ExpressivLookLeft = 13, ExpressivLookRight = 14, ExpressivWinkLeft = 15, ExpressivWinkRight = 16, ExpressivBlink = 17, CognitivNeutral = 18
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
                    controlEngine.addCommand(new Command((int)command.CognitivPush, command.CognitivPush.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    success = controlEngine.addCommand(new Command((int)command.CognitivPull, command.CognitivPull.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitivLift, command.CognitivLift.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    success = controlEngine.addCommand(new Command((int)command.CognitivDrop, command.CognitivDrop.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitivLeft, command.CognitivLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRight, command.CognitivRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_LEFT:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateLeft, command.CognitivRotateLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_RIGHT:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateRight, command.CognitivRotateRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_CLOCKWISE:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateClockwise, command.CognitivRotateClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_COUNTER_CLOCKWISE:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateCounterClockwise, command.CognitivRotateCounterClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_FORWARDS:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateForward, command.CognitivRotateForward.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_REVERSE:
                    success = controlEngine.addCommand(new Command((int)command.CognitivRotateBackwards, command.CognitivRotateBackwards.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    success = controlEngine.addCommand(new Command((int)command.CognitivNeutral, command.CognitivNeutral.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
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
                if(!controlEngine.addCommand(new Command((int)command.ExpressivSmile, command.ExpressivSmile.ToString("G"), this.id, e.emoState.ExpressivGetSmileExtent())))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivSmile.ToString("G"))));
                }
            if (e.emoState.ExpressivIsLookingLeft() != lastEmoState.ExpressivIsLookingLeft())
                if(!controlEngine.addCommand(new Command((int)command.ExpressivLookLeft, command.ExpressivLookLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivLookLeft.ToString("G"))));
                }
            if (e.emoState.ExpressivIsLookingRight() != lastEmoState.ExpressivIsLookingRight())
                if(!controlEngine.addCommand(new Command((int)command.ExpressivLookRight, command.ExpressivLookRight.ToString("G"), this.id, 1.0)))
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivLookRight.ToString("G"))));
            }
            if (e.emoState.ExpressivIsLeftWink() != lastEmoState.ExpressivIsLeftWink())
                if(!controlEngine.addCommand(new Command((int)command.ExpressivWinkLeft, command.ExpressivWinkLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivWinkLeft.ToString("G"))));
                }
            if (e.emoState.ExpressivIsRightWink() != lastEmoState.ExpressivIsRightWink())
                if (!controlEngine.addCommand(new Command((int)command.ExpressivWinkRight, command.ExpressivWinkRight.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivWinkRight.ToString("G"))));
                }

            if (e.emoState.ExpressivIsBlink() != lastEmoState.ExpressivIsBlink())
                if(!controlEngine.addCommand(new Command((int)command.ExpressivBlink, command.ExpressivBlink.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, command.ExpressivBlink.ToString("G"))));
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

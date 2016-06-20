using Emotiv;
using System;
using System.Text;
using System.Threading;

namespace EmotivEngine
{
    class EmoController : IController
    {
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<WarningEventArgs> Warning;
        private static string type = Texts.ControllerTypes.CT_EmotivEPOC;
        private CentralControlEngine cce;
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


        private enum commands : int
        {
            CognitivPush = 0, CognitivPull = 1, CognitivLift = 2, CognitivDrop = 3, CognitivLeft = 4,
            CognitivRight = 5, CognitivRotateLeft = 6, CognitivRotateRight = 7, CognitivRotateClockwise = 8,
            CognitivRotateCounterClockwise = 9, CognitivRotateForward = 10, CognitivRotateBackwards = 11,
            ExpressivSmile = 12, ExpressivLookLeft = 13, ExpressivLookRight = 14, ExpressivWinkLeft = 15, ExpressivWinkRight = 16, ExpressivBlink = 17, CognitivNeutral = 18
        };

        private EmoController(CentralControlEngine cce) { this.cce = cce; }


        public static IController getInstance(CentralControlEngine cce)
        {
            if (singleInstance != null)
                return singleInstance;
            return singleInstance = new EmoController(cce);

        }

        public string[] getCommands()
        {
            return (string[])Enum.GetNames(typeof(commands));
        }

        public bool initialize()
        {
            engine = EmoEngine.Instance;
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);
            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(connected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(disconnected);

            try {
                engine.RemoteConnect("192.168.178.199", 3008);
            }
            catch(Emotiv.EmoEngineException e)
            {
                cce.addLog(this.Name, e.Message, Logger.loggingLevel.error);
                return false;
            }
            return true;
        }

        private void Engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {

            cce.addLog(this.Name, new StringBuilder().Append("Received cognitive: ").Append(e.emoState.CognitivGetCurrentAction()).Append(" with power ").Append(e.emoState.CognitivGetCurrentActionPower()).ToString(), Logger.loggingLevel.debug);
            bool success = true;
            switch (e.emoState.CognitivGetCurrentAction())
            {
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    cce.addCommand(new Command((int)commands.CognitivPush, commands.CognitivPush.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    success = cce.addCommand(new Command((int)commands.CognitivPull, commands.CognitivPull.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    success = cce.addCommand(new Command((int)commands.CognitivLift, commands.CognitivLift.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    success = cce.addCommand(new Command((int)commands.CognitivDrop, commands.CognitivDrop.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    success = cce.addCommand(new Command((int)commands.CognitivLeft, commands.CognitivLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    success = cce.addCommand(new Command((int)commands.CognitivRight, commands.CognitivRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_LEFT:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateLeft, commands.CognitivRotateLeft.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_RIGHT:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateRight, commands.CognitivRotateRight.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_CLOCKWISE:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateClockwise, commands.CognitivRotateClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_COUNTER_CLOCKWISE:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateCounterClockwise, commands.CognitivRotateCounterClockwise.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_FORWARDS:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateForward, commands.CognitivRotateForward.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_REVERSE:
                    success = cce.addCommand(new Command((int)commands.CognitivRotateBackwards, commands.CognitivRotateBackwards.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    success = cce.addCommand(new Command((int)commands.CognitivNeutral, commands.CognitivNeutral.ToString("G"), this.id, (double)e.emoState.CognitivGetCurrentActionPower()));
                    break;
                //default:
                  //  success = true;
                  //  controlEngine.addLog(Name, String.Format(Texts.Logging.unhandledCommand, e.emoState.CognitivGetCurrentAction()), Logger.loggingLevel.warning);
                  //  break;
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

            cce.addLog(this.Name, new StringBuilder().Append("Received EmoEngineStateUpdated").ToString(), Logger.loggingLevel.debug);
            if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.BAD_SIGNAL)
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning != null)
                    lclWarning(this, new WarningEventArgs(Texts.WarningMessages.badSignal));
            }
            else if (e.emoState.GetWirelessSignalStatus() == EdkDll.EE_SignalStrength_t.NO_SIGNAL)
            {
                EventHandler<WarningEventArgs> lclWarning = Warning;
                if (lclWarning!= null)
                    lclWarning(this, new WarningEventArgs(Texts.ErrorMessages.noSignal));
            }
        }

        private void Engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {

            double smileThreshold = 0.1;
            if (e.emoState.ExpressivGetSmileExtent() != lastEmoState.ExpressivGetSmileExtent() && (double)e.emoState.ExpressivGetSmileExtent() > smileThreshold)
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivSmile.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivSmile, commands.ExpressivSmile.ToString("G"), this.id, e.emoState.ExpressivGetSmileExtent())))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivSmile.ToString("G"))));
                }
            }
            if (e.emoState.ExpressivIsLookingLeft() != lastEmoState.ExpressivIsLookingLeft())
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivLookLeft.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivLookLeft, commands.ExpressivLookLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivLookLeft.ToString("G"))));
                }
            }
            if (e.emoState.ExpressivIsLookingRight() != lastEmoState.ExpressivIsLookingRight())
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivLookRight.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivLookRight, commands.ExpressivLookRight.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivLookRight.ToString("G"))));
                }
            }
            if (e.emoState.ExpressivIsLeftWink() != lastEmoState.ExpressivIsLeftWink())
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivWinkLeft.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivWinkLeft, commands.ExpressivWinkLeft.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivWinkLeft.ToString("G"))));
                }
            }
            if (e.emoState.ExpressivIsRightWink() != lastEmoState.ExpressivIsRightWink())
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivWinkRight.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivWinkRight, commands.ExpressivWinkRight.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivWinkRight.ToString("G"))));
                }

            }
            if (e.emoState.ExpressivIsBlink() != lastEmoState.ExpressivIsBlink())
            {
                cce.addLog(this.Name, new StringBuilder().Append("Received expressive: ").Append(commands.ExpressivBlink.ToString("G")).ToString(), Logger.loggingLevel.debug);
                if (!cce.addCommand(new Command((int)commands.ExpressivBlink, commands.ExpressivBlink.ToString("G"), this.id, 1.0)))
                {
                    EventHandler<WarningEventArgs> lclWarning = Warning;
                    if (lclWarning != null)
                        lclWarning(this, new WarningEventArgs(String.Format(Texts.WarningMessages.couldntAddcommand, commands.ExpressivBlink.ToString("G"))));
                }
            }
        }

        private void Engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            cce.addLog(this.Name, new StringBuilder().Append("Received affectiv - affectives are not covered by this version of the software.").ToString(), Logger.loggingLevel.debug);
            //bool success = true;
            //if (!success)
            //{
            //    EventHandler<WarningEventArgs> lclWarning = Warning;
            //    if (lclWarning != null)
            //        lclWarning(this, new WarningEventArgs(Texts.WarningMessages.couldntAddcommand));
            //}
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
            runEngineThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    engine.ProcessEvents();
                    Thread.Sleep(10);
                }
            }));
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

        private void connected(object sender, EmoEngineEventArgs e)
        {
            if(cce != null)
                cce.addLog(this.Name, String.Format(Texts.Logging.connected, "engine"), Logger.loggingLevel.info);
        }

        private void disconnected(object sender, EmoEngineEventArgs e)
        {
            if (cce != null)
                cce.addLog(this.Name, String.Format(Texts.Logging.disconnected, "engine"), Logger.loggingLevel.info);
        }

        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            if (cce != null)
                cce.addLog(this.Name, String.Format(Texts.Logging.emotivUserAdded, e.userId), Logger.loggingLevel.debug);
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

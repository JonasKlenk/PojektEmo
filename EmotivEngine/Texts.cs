using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    static class Texts
    {
        public static class Logging
        {
            static public string controllerRegistered = "Controller {0} with internal ID {1} registered.";
            static public string controllerUnregistered = "Controller {0} with internal ID {1} unregistered.";
            static public string controllableRegistered = "Controllable device {0} with internal ID {1} registered.";
            static public string controllableUnregistered = "Controllable device {0} with internal ID {1} unregistered.";
            static public string commandAddedToQueue = "Command {0} (id: {1}) from sender {2} with itensity {3} added.";
            static public string engineStarted = "Engine started succesfully.";
            static public string engineStoppedByUser = "Engine stopped by user";
            static public string mapRegistered = "Map {0} registered.";
            static public string mapUnregistered = "Map {0} unregisteerd.";
        }

        public static class GUITexts
        {

        }

        public static class ControllerTypes
        {
            static public string CT_EmotivEPOC = "Emotiv EPOC";
            static public string CT_EmotivInsight = "Emotiv Insight";
            static public string CT_MicrosoftController360 = "Microsoft Controller XBOX 360";
        }
    }
}

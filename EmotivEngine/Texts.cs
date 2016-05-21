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
            static public string controllerRegisterd = "Controller {0} with internal ID {1} registerd.";
            static public string controllerUnregisterd = "Controller {0} with internal ID {1} unregisterd.";
            static public string controllableRegisterd = "Controllable device {0} with internal ID {1} registerd.";
            static public string controllableUnregisterd = "Controllable device {0} with internal ID {1} unregisterd.";
            static public string commandAddedToQueue = "Command {0} (id: {1}) from sender {2} with itensity {3} added.";
            static public string engineStarted = "Engine started succesfully.";
            static public string engineStoppedByUser = "Engine stopped by user";
        }

        public static class GUITexts
        {

        }
    }
}

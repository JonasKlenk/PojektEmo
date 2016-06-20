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
            static public string mapUnregistered = "Map {0} unregistered.";
            static public string categoryRegistered = "Device Category {0} registered";
            static public string unhandledCommand = "Signal is not handled by this driver: {0}.";
            static public string receivedCommand = "Received command {0}with id {1}";
            static public string connected = "Connected to {0}";
            static public string disconnected = "Disconnected from {0}";
            static public string emotivUserAdded = "User with id {0} added!";
            static public string logLevelChanged = "Log level changed from {0} to {1}";
            static public string controllerRegistrationError = "Could not Register Controller {0}";
        }

        public static class GUITexts
        {
            static public string exception = "Map not fund or invalid.";
            static public string cannotUnbindEngineRunning = "Cannot remove binding while engine still running. It is necessary to first stop the engine.";
            static public string errorUnbindCaption = "Error unbinding";
        }

        public static class ControllerTypes
        {
            static public string CT_EmotivEPOC = "Emotiv EPOC";
            static public string CT_EmotivInsight = "Emotiv Insight";
            static public string CT_MicrosoftController360 = "Microsoft Controller XBOX 360";
        }

        public static class WarningMessages
        {
            static public string defaultMessage = "No warning message given by event creator";
            static public string badSignal = "Bad wireless signal quality.";
            static public string couldntAddcommand = "Could not add command {0} to queue.";
        }

        public static class ErrorMessages
        {
            static public string defaultMessage = "No error message given by event creator";
            static public string noSignal = "No wireless signal.";
        }
    }
}

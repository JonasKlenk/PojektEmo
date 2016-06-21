using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Texts for programme
    /// </summary>
    static class Texts
    {
        /// <summary>
        /// Texts for logging
        /// </summary>
        public static class Logging
        {
            /// <summary>
            /// The controller registered
            /// </summary>
            static public string controllerRegistered = "Controller {0} with internal ID {1} registered.";
            /// <summary>
            /// The controller unregistered
            /// </summary>
            static public string controllerUnregistered = "Controller {0} with internal ID {1} unregistered.";
            /// <summary>
            /// The controllable registered
            /// </summary>
            static public string controllableRegistered = "Controllable device {0} with internal ID {1} registered.";
            /// <summary>
            /// The controllable unregistered
            /// </summary>
            static public string controllableUnregistered = "Controllable device {0} with internal ID {1} unregistered.";
            /// <summary>
            /// The command added to queue
            /// </summary>
            static public string commandAddedToQueue = "Command {0} (id: {1}) from sender {2} with itensity {3} added.";
            /// <summary>
            /// The engine started
            /// </summary>
            static public string engineStarted = "Engine started succesfully.";
            /// <summary>
            /// The engine stopped by user
            /// </summary>
            static public string engineStoppedByUser = "Engine stopped by user";
            /// <summary>
            /// The map registered
            /// </summary>
            static public string mapRegistered = "Map {0} registered.";
            /// <summary>
            /// The map unregistered
            /// </summary>
            static public string mapUnregistered = "Map {0} unregistered.";
            /// <summary>
            /// The category registered
            /// </summary>
            static public string categoryRegistered = "Device Category {0} registered";
            /// <summary>
            /// The unhandled command
            /// </summary>
            static public string unhandledCommand = "Signal is not handled by this driver: {0}.";
            /// <summary>
            /// The received command
            /// </summary>
            static public string receivedCommand = "Received command {0}with id {1}";
            /// <summary>
            /// The connected
            /// </summary>
            static public string connected = "Connected to {0}";
            /// <summary>
            /// The disconnected
            /// </summary>
            static public string disconnected = "Disconnected from {0}";
            /// <summary>
            /// The emotiv user added
            /// </summary>
            static public string emotivUserAdded = "User with id {0} added!";
            /// <summary>
            /// The log level changed
            /// </summary>
            static public string logLevelChanged = "Log level changed from {0} to {1}";
            /// <summary>
            /// The controller registration error
            /// </summary>
            static public string controllerRegistrationError = "Could not Register Controller {0}";
        }

        /// <summary>
        /// Texts for GUI
        /// </summary>
        public static class GUITexts
        {

            /// <summary>
            /// The map exception
            /// </summary>
            static public string MapException = "Map not fund or invalid.";
            /// <summary>
            /// The cannot unbind engine running
            /// </summary>
            static public string cannotUnbindEngineRunning = "Cannot remove binding while engine still running. It is necessary to first stop the engine.";
            /// <summary>
            /// The error unbind caption
            /// </summary>
            static public string errorUnbindCaption = "Error unbinding";
        }

        /// <summary>
        /// Controller type names
        /// </summary>
        public static class ControllerTypes
        {
            /// <summary>
            /// The c t_ emotiv epoc
            /// </summary>
            static public string CT_EmotivEPOC = "Emotiv EPOC";
            /// <summary>
            /// The c t_ emotiv insight
            /// </summary>
            static public string CT_EmotivInsight = "Emotiv Insight";
            /// <summary>
            /// The c t_ microsoft controller360
            /// </summary>
            static public string CT_MicrosoftController360 = "Microsoft Controller XBOX 360";
        }

        /// <summary>
        /// Texts for warning messages
        /// </summary>
        public static class WarningMessages
        {
            /// <summary>
            /// The default message
            /// </summary>
            static public string defaultMessage = "No warning message given by event creator";
            /// <summary>
            /// The bad signal
            /// </summary>
            static public string badSignal = "Bad wireless signal quality.";
            /// <summary>
            /// The couldnt addcommand
            /// </summary>
            static public string couldntAddcommand = "Could not add command {0} to queue.";
        }

        /// <summary>
        /// Texts for error messages
        /// </summary>
        public static class ErrorMessages
        {
            /// <summary>
            /// The default message
            /// </summary>
            static public string defaultMessage = "No error message given by event creator";
            /// <summary>
            /// The no signal
            /// </summary>
            static public string noSignal = "No wireless signal.";
        }
    }
}

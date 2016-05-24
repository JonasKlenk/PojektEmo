using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    /// <summary>
    /// Interface for basic commands and util functions any device should support
    /// </summary>
    interface IControllableDevice
    {
        /// <summary>
        /// Device ID
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Device name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Execute command on device
        /// </summary>
        /// <param name="action">Command object</param>
        void performAction(Command action);

        /// <summary>
        /// Send device into emergency mode (device will try to avoid collision or other harmful actions)
        /// </summary>
        void enterFallbackMode();

        /// <summary>
        /// Returns category type of the device
        /// </summary>
        /// <returns>category type of the device</returns>
        DeviceCategory getCategory();
        
        /// <summary>
        /// Returns a list of actions the device can perform
        /// </summary>
        /// <returns>List of available actions</returns>
        string[] getActions();

        /// <summary>
        /// Returns if device is ready to perform actions
        /// </summary>
        /// <returns>Boolean if device is ready</returns>
        bool isReady();

        /// <summary>
        /// Activate device
        /// </summary>
        void setActive();

        /// <summary>
        /// Deactivate device
        /// </summary>
        void setDeactive();
    }
}

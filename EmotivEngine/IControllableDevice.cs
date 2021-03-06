﻿using System;

namespace EmotivEngine
{
    /// <summary>
    /// Interface for basic commands and util functions any device should support
    /// </summary>
    interface IControllableDevice
    {

        event EventHandler<WarningEventArgs> Warning;
        event EventHandler<ErrorEventArgs> Error;
        /// <summary>
        /// Device ID
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Device name
        /// </summary>
        string Name { get; }

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
        /// Returns if device is ready to perform actions, is called by engine before setting active!
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

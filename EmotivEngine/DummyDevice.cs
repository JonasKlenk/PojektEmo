using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmotivEngine
{
    /// <summary>
    /// A dummy of a ControllableDevice
    /// </summary>
    /// <seealso cref="EmotivEngine.IControllableDevice" />
    class DummyDevice : IControllableDevice
    {
        /// <summary>
        /// Initializes a DummyCategory for the DummyDevice.
        /// </summary>
        DeviceCategory c = new DeviceCategory("DummyCategory", new string[] { "RechtenArmheben", "LinkenArmHeben", "LinkesBeinHeben", "RechtesBeinHeben", "Umfallen" });

        /// <summary>
        /// The identifier
        /// </summary>
        private int id = 42;
        /// <summary>
        /// The single instance
        /// </summary>
        static private DummyDevice singleInstance = null;
        /// <summary>
        /// The control engine
        /// </summary>
        private CentralControlEngine controlEngine;

        /// <summary>
        /// Occurs when [warning].
        /// </summary>
        public event EventHandler<WarningEventArgs> Warning;
        /// <summary>
        /// Occurs when [error].
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        /// Device ID
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                this.id=value;
            }
        }

        /// <summary>
        /// Device name
        /// </summary>
        public string Name
        {
            get
            {
                return c.getCategoryName() + " (ID: " + Id + ")";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyDevice"/> class.
        /// </summary>
        /// <param name="cce">The cce.</param>
        internal DummyDevice(CentralControlEngine cce) { this.controlEngine = cce; }

        /// <summary>
        /// Send device into emergency mode (device will try to avoid collision or other harmful actions)
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void enterFallbackMode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of actions the device can perform
        /// </summary>
        /// <returns>
        /// List of available actions
        /// </returns>
        public string[] getActions()
        {
            return c.ActionList;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        public int getId()
        {
            return this.id;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="cce">The cce.</param>
        /// <returns></returns>
        public static IControllableDevice getInstance(CentralControlEngine cce)
        {
            if (singleInstance != null)
                return singleInstance;
            return singleInstance = new DummyDevice(cce);
        }

        /// <summary>
        /// Returns category type of the device
        /// </summary>
        /// <returns>
        /// category type of the device
        /// </returns>
        public DeviceCategory getCategory()
        {
            return c;
        }

        /// <summary>
        /// Returns if device is ready to perform actions, is called by engine before setting active!
        /// </summary>
        /// <returns>
        /// Boolean if device is ready
        /// </returns>
        public bool isReady()
        {
            //throw new NotImplementedException();
            return true;
        }

        /// <summary>
        /// Execute command on device
        /// </summary>
        /// <param name="action">Command object</param>
        public void performAction(Command action)
        {
            controlEngine.addLog(Name, String.Format(Texts.Logging.receivedCommand, action.getCommandName(), action.getCommandName()), Logger.loggingLevel.debug);
        }

        /// <summary>
        /// Activate device
        /// </summary>
        public void setActive()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Deactivate device
        /// </summary>
        public void setDeactive()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void setId(int id)
        {
            this.id = id;
        }
    }
}
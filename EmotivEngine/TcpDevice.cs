using System;
using System.Net.Sockets;
using System.Text;

namespace EmotivEngine
{
    /// <summary>
    /// Controllable device with connection via TCP Sockets
    /// </summary>
    class TcpDevice : IControllableDevice
    {
        private int id;
        private DeviceCategory deviceCategory;
        private CentralControlEngine cce;
        private TcpClient client;
        private NetworkStream stream;

        public event EventHandler<WarningEventArgs> Warning;
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        /// ID of this device
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of this device
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates an instance of TcpDevice and opens socket stream immediately
        /// Fetches DeviceCategory over TCP connection, which is later available under getCategory()
        /// </summary>
        /// <param name="cce">Reference to calling CentralControlEngine</param>
        /// <param name="deviceIp">Remote IP. Used to open socket stream</param>
        /// <param name="devicePort">Remote Port used for socket stream</param>
        public TcpDevice(CentralControlEngine cce, string deviceIp, int devicePort)
        {
            this.cce = cce;
            client = new TcpClient(deviceIp, devicePort);
            stream = client.GetStream();
            deviceCategory = cce.findCategoryByName(SendAndReceive("type"));
        }

        /// <summary>
        /// Destructor
        /// Closes socket stream
        /// </summary>
        ~TcpDevice()
        {
            client.Close();
        }

        /// <summary>
        /// Internal function to send a command via TCP connection
        /// </summary>
        /// <param name="command">Command to be sent. Formating depends on the receiving device</param>
        private void SendCommmand(string command)
        {
            Console.WriteLine("Sending : " + command);
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(command);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        /// <summary>
        /// Internal function to send a command and immediately receice an answer via TCP connection
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Answer from remote device as string</returns>
        private string SendAndReceive(string command)
        {
            SendCommmand(command);
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            string answer = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            Console.WriteLine("Received : " + answer);
            return answer;
        }

        /// <summary>
        /// Sends device into emergency mode (device will try to avoid collision or other harmful actions)
        /// </summary>
        public void enterFallbackMode()
        {
            SendCommmand("fallback");
        }

        /// <summary>
        /// Returns a list of actions available for this device category
        /// </summary>
        /// <returns>List of available actions</returns>
        public string[] getActions()
        {
            return deviceCategory.actionList;
        }

        /// <summary>
        /// Get device category
        /// </summary>
        /// <returns>Device category</returns>
        public DeviceCategory getCategory()
        {
            return deviceCategory;
        }

        /// <summary>
        /// Returns if device is ready to receive controlling commands
        /// </summary>
        /// <returns>Boolean if device is ready</returns>
        public bool isReady()
        {
            return Convert.ToBoolean(SendAndReceive("ready"));
        }

        /// <summary>
        /// Perform spefic action
        /// </summary>
        /// <param name="action">Action as command object</param>
        public void performAction(Command action)
        {
            SendCommmand(action.getCommandId() + ";" + action.getIntensity());
        }

        /// <summary>
        /// Set device as active
        /// </summary>
        public void setActive()
        {
            SendCommmand("activate");
        }

        /// <summary>
        /// Set device as deactive
        /// </summary>
        public void setDeactive()
        {
            SendCommmand("deactivate");
        }
    }
}

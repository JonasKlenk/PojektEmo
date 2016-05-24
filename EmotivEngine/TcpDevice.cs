using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    class TcpDevice : IControllableDevice
    {
        private int id;
        private DeviceCategory deviceCategory;
        private CentralControlEngine cce;
        private TcpClient client;
        private NetworkStream stream;

        public int Id { get; set; }
        public string Name { get; set; }

        public TcpDevice (CentralControlEngine cce, string deviceIp, int devicePort)
        {
            this.cce = cce;
            client = new TcpClient(deviceIp, devicePort);
            stream = client.GetStream();
            deviceCategory = cce.findCategoryByName(SendAndReceive("type"));
        }

        ~TcpDevice()
        {
            client.Close();
        }

        private void SendCommmand(string command)
        {
            Console.WriteLine("Sending : " + command);
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(command);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private string SendAndReceive(string command)
        {
            SendCommmand(command);
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = stream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            string answer = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            Console.WriteLine("Received : " + answer);
            return answer;
        }

        public void enterFallbackMode()
        {
            SendCommmand("fallback");
        }

        public string[] getActions()
        {
            return deviceCategory.actionList;
        }

        public DeviceCategory getType()
        {
            return deviceCategory;
        }

        public bool isReady()
        {
            return Convert.ToBoolean(SendAndReceive("ready"));
        }

        public void performAction(Command action)
        {
            SendCommmand(action.getCommandId() + ";" + action.getIntensity());
        }

        public void setActive()
        {
            SendCommmand("activate");
        }

        public void setDeactive()
        {
            SendCommmand("deactivate");
        }
    }
}

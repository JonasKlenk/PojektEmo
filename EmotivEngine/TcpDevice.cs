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
            throw new NotImplementedException();
        }

        public string[] getActions()
        {
            throw new NotImplementedException();
        }

        public DeviceCategory getType()
        {
            return cce.findCategoryByName("Drohne");
        }

        public void initialize()
        {
            throw new NotImplementedException();
        }

        public bool isReady()
        {
            throw new NotImplementedException();
        }

        public void performAction(Command action)
        {
            throw new NotImplementedException();
        }

        public bool setActive()
        {
            throw new NotImplementedException();
        }

        public bool setDeactive()
        {
            throw new NotImplementedException();
        }
    }
}

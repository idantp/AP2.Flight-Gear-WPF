using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace FlightSimulator.Model
{
    public class Commands
    {
        private TcpClient client;
        private NetworkStream stream;

        public Commands() { }

        public void connect(string ip, int portNumber)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), portNumber);
            this.client = new TcpClient();
            while (!client.Connected)
            {
                try { client.Connect(ep); }
                catch (Exception) { }
            }
            using (this.stream = client.GetStream()) { }
        }

        public void openClientThread()
        {
            new Thread(delegate () {
                connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);}).Start();
        }

        public void commandsSetter(string commands)
        {
            if (client.Connected)
            {
                int i;
                string[] separatedCommands = commands.Split('\n');
                for (i = 0; i < separatedCommands.Length; i++) {
                    separatedCommands[i] += "\r\n";
                }

                foreach (string cmd in separatedCommands)
                {
                    //TODO make sure it works
                    using (BinaryWriter writer = new BinaryWriter(this.stream))
                    {
                        writer.Write(cmd);
                    }
                    System.Threading.Thread.Sleep(2000);
                }
            }
            return;
        }
    }
}

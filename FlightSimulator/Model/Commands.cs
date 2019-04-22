using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace FlightSimulator.Model
{
    public class Commands
    {
        private TcpClient client;
        private NetworkStream stream;
        private static Commands commandsInstance = null;
        private bool isConnected = false;

        public Commands() { }

        //Singleton
        public static Commands CommandsInstance {
            get {
                if (commandsInstance == null) {
                    commandsInstance = new Commands();
                }
                return commandsInstance;
            }
           
        }

        public void connect(string ip, int portNumber)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), portNumber);
            this.client = new TcpClient();
            while (!client.Connected)
            {
                try {
                    client.Connect(ep);
                }
                catch (Exception) { }
            }
            isConnected = true;
            this.stream = client.GetStream();
        }

        public void openClientThread()
        {
            new Thread(delegate () {
                connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);}).Start();
        }

        public void sendCommands(string commands)
        {
            if (isConnected)
            {
                if (!string.IsNullOrEmpty(commands))
                {
                    int i;
                    string[] separatedCommands = commands.Split('\n');
                    for (i = 0; i < separatedCommands.Length; i++)
                    {
                        separatedCommands[i] += "\r\n";
                    }
                    if (this.stream.CanWrite)
                    {
                        foreach (string cmd in separatedCommands)
                        {
                            Byte[] bytesToWrite = Encoding.ASCII.GetBytes(cmd);
                            stream.Write(bytesToWrite, 0, bytesToWrite.Length);
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
        }
    }
}

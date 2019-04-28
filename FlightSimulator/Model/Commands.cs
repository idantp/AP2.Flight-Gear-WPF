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

        public void disconnect(){
            if (isConnected)
            {
                isConnected = false;
                client.Close();
            }
        }

        /*
        * Function Name: connect
        * Function Input: string ip, int portNumber
        * Function Output: None
        * Function Operatin: the function gets an IP Adress and a Port number and
        *                    makes a client connection to the flight simlutaor(the server).
        */
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

        /*
        * Function Name: openClientThread
        * Function Input: None
        * Function Output: None
        * Function Operatin: the function open a TCP client in a new thread.
        */
        public void openClientThread()
        {
            new Thread(delegate () {
                connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);}).Start();
        }

        /*
        * Function Name: sendCommands
        * Function Input: string commands
        * Function Output: None
        * Function Operatin: the function gets a commands instructions and sets them
        *                    in the flight simulator.
        */
        public void sendCommands(string commands)
        {
            if (isConnected)
            {
                // if there are no commands, then return
                if (!string.IsNullOrEmpty(commands))
                {
                    // for each command - replace Unix NewLine with Windows NewLine
                    int i;
                    string[] separatedCommands = commands.Split('\n');
                    for (i = 0; i < separatedCommands.Length; i++)
                    {
                        separatedCommands[i] += "\r\n";
                    }
                    if (this.stream.CanWrite)
                    {
                        // send any command that was found every 2 seconds
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

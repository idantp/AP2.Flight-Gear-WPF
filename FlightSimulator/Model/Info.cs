using System;
using FlightSimulator.ViewModels;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace FlightSimulator.Model

{
    class Info : BaseNotify
    {
        private double throttle;
        private double rudder;
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        Nullable<float> latitude = null;
        Nullable<float> longtitude = null;
        int lonCounter = 0;
        int latCounter = 0;
        bool run;

        //constructor
        private Info() {
            throttle = 0;
            rudder = 0;
            run = true;
        }
        //Singleton
        private static Info serverInstance = null;
        public static Info ServerInstance {
            get {
                if (serverInstance == null) {
                    serverInstance = new Info(); 
                }
                return serverInstance;
            }
        }
        //Properties below.
        public double Throttle {
            get {
                return throttle;
            }
            set {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public Nullable<float> Lon
        {
            get
            {
                return longtitude;
            }
            set
            {
                this.longtitude = value;
                //so that the first two values from the server don't count in the drawing.
                if (lonCounter < 2)
                {
                    lonCounter++;
                }
                //only notify after sampling twice from the server.
                else
                {
                    NotifyPropertyChanged("Lon");
                }
            }
        }
        public Nullable<float> Lat
        {
            get
            {
                return latitude;
            }
            set
            {
                this.latitude = value;
                //so that the first two values from the server don't count in the drawing.
                if (latCounter < 2)
                {
                    latCounter++;
                }
                //only notify after sampling twice from the server.
                else
                {
                    NotifyPropertyChanged("Lat");
                }
            }
        }

            /*
             * Function Name: connectAsServer
             * Function Input: None
             * Function Output: None
             * Function Operatin: the function opens a server from which it reads data
             */
            public void connectAsServer() {
            // TCP Server
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                           Properties.Settings.Default.FlightInfoPort);
            tcpListener = new TcpListener(ep);
            tcpListener.Start();
            this.tcpClient = tcpListener.AcceptTcpClient();
            new Thread(() => getFlightData()).Start();
        }

        /*
         * Function Name: closeServer
         * Function Input: none
         * Function Output: none
         * Function Operatin: the function close the server that this class stores
         */
        public void closeServer() {
            this.tcpClient.Close();
            this.tcpListener.Stop();
           
        }

        public void disconnect()
        {
            run = false;
        }

        /*
         * Function Name: getFlightData
         * Function Input: none
         * Function Output: none
         * Function Operatin: the function reads the current data from the server,
         *                    and parses it to latitude, longitude, rudder and throttle
         */
        public void getFlightData() {
            using (NetworkStream stream = tcpClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (run){
                    // stores command line from auto pilot mode
                    string line = "";
                    char c;
                    while ((c = reader.ReadChar()) != '\n'){
                        line += c;
                    }
                    // if no command was found - return
                    if (line == ""){
                        break;
                    }
                    //parsing longitude, latitude, rudder and throttle
                    string[] values = line.Split(',');
                    Lat = float.Parse(values[1]);
                    Lon = float.Parse(values[0]);
                    Rudder = double.Parse(values[21]);
                    Throttle = double.Parse(values[23]);
                }
                closeServer();
            }
        }
    }
}

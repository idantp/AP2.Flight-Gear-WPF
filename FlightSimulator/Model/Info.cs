using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulator.Model
{
    class Info
    {
        private double throttle;
        private double rudder;
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        Nullable<float> latitude = null;
        Nullable<float> longtitude = null;
        bool run;


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
        public Nullable<float> Longtitude
        {
            get
            {
                return longtitude;
            }
            set
            {
                this.longtitude = value;
                NotifyPropertyChanged("Longtitude");
            }
        }
        public Nullable<float> Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                this.longtitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        public void connectAsServer() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                           Properties.Settings.Default.FlightInfoPort);
            tcpListener = new TcpListener(ep);
            tcpListener.Start();
            tcpClient = tcpListener.AcceptTcpClient();
            new Thread(() => getFlightData(tcpListener, tcpClient)).Start();
        }

        public void closeServer() {
            this.tcpClient.Close();
            this.tcpListener.Stop();
        }

        public void getFlightData(TcpListener tcpListener, TcpClient tcpClient) {
            using (NetworkStream stream = tcpClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
               
                while (run) {
                    string line = "";
                    char c;
                    while (c = reader.ReadChar() != '\n')
                    {
                        line += c;
                    }
                    if (line == "") {
                        closeServer();
                        break;
                    }
                    string[] values = line.Split(",");
                    Latitude = float.Parse(values[1]);
                    Longtitude = float.Parse(values[0]);
                    Rudder = double.Parse(values[21]);
                    Throttle = double.Parse(values[23]);
                    
                }
            }
        }
    }
}

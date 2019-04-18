﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualControlViewModel: BaseNotify
    {
        private string rudder, elevator, aileron, throttle;


        public ManualControlViewModel() {
            
            rudder = "set /controls/flight/rudder ";
            elevator = "set /controls/flight/elevator ";
            aileron = "set /controls/flight/aileron ";
            throttle = "set /controls/engines/current-engine/throttle ";
        }

        public double Rudder {
            set {
            }
            get {
            }
        }
        public double Throttle
        {
            set
            {
            }
            get
            {
            }
        }
        public double Aileron
        {
            set
            {
            }
            get
            {
            }
        }
    }
    public double Elevator
    {
        set
        {
        }
        get
        {
        }
    }
}
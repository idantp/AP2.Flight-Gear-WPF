using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.ViewModels;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class ManualControlViewModel : BaseNotify
    {
        private ManualControlModel controlModel;

        public ManualControlViewModel()
        {
            controlModel = new ManualControlModel();
            controlModel.PropertyChanged += PropertyChangedReached;
        }

        private void PropertyChangedReached(object sender, System.ComponentModel.PropertyChangedEventArgs ev)
        {
            NotifyPropertyChanged(ev.PropertyName);
        }

        public double Rudder
        {
            get
            {
                return controlModel.Rudder;
                
            }
            set
            {
                new Thread(() => Commands.CommandsInstance.sendCommands("set /controls/flight/rudder " + value.ToString())).Start();
            }
        }

        public double Throttle
        {
            get
            {
                return controlModel.Throttle;
            }
            set
            {
                new Thread(() => Commands.CommandsInstance.sendCommands("set /controls/engines/current-engine/throttle " + value.ToString())).Start();
            }
            
        }
        public double Aileron
        {
            set
            {
                new Thread(() => Commands.CommandsInstance.sendCommands("set /controls/flight/aileron " + value.ToString())).Start();
            }
        }

        public double Elevator
        {
            set
            {
                new Thread(() => Commands.CommandsInstance.sendCommands("set /controls/flight/elevator " + value.ToString())).Start();
            }
        }
    }
}

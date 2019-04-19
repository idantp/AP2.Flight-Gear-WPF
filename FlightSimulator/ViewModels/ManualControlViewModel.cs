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
                String temp = "set /controls/flight/rudder ";
                temp += value.ToString();
                new Thread(() => Commands.CommandsInstance.sendCommands(temp)).Start();
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
                String temp = "set /controls/engines/current-engine/throttle ";
                temp += value.ToString();
                new Thread(() => Commands.CommandsInstance.sendCommands(temp)).Start();
            }
            
        }
        public double Aileron
        {
            set
            {
                String temp = "set /controls/flight/aileron ";
                temp += value.ToString();
                new Thread(() => Commands.CommandsInstance.sendCommands(temp)).Start();
            }
        }

        public double Elevator
        {
            set
            {
                String temp = "set /controls/flight/elevator ";
                temp += value.ToString();
                new Thread(() => Commands.CommandsInstance.sendCommands(temp)).Start();
            }
        }
    }
}

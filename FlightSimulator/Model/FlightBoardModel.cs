using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Views;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    class FlightBoardModel : BaseNotify
    {
        public FlightBoardModel() {
            Info.ServerInstance.PropertyChanged += PropertyChangedReached;
        }

        private void PropertyChangedReached(object sender, System.ComponentModel.PropertyChangedEventArgs ev)
        {
            NotifyPropertyChanged(ev.PropertyName);
        }

        public Nullable<float> Lon
        {
            get {
                return Info.ServerInstance.Lon;
            }
            set { }
        }

        public Nullable<float> Lat
        {
            get {
                return Info.ServerInstance.Lat;
            }
            set { }
        }
    }
}

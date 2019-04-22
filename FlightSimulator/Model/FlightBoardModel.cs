using System;
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

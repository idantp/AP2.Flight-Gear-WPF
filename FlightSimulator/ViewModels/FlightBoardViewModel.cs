using System;

using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel flightBoardModel;
        public FlightBoardViewModel() {
            flightBoardModel = new FlightBoardModel();
            flightBoardModel.PropertyChanged += PropertyChangedReached;
        }

        private void PropertyChangedReached(object sender, System.ComponentModel.PropertyChangedEventArgs ev)
        {
            NotifyPropertyChanged(ev.PropertyName);
        }

        public Nullable<float> Lon
        {
            get {
                return flightBoardModel.Lon;
            }
            set { }
        }

        public Nullable<float> Lat
        {
            get {
                return flightBoardModel.Lat;
            }
            set { }
        }
    }
}

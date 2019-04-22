using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    class ManualControlModel : BaseNotify
    {

        public ManualControlModel() {
            Info.ServerInstance.PropertyChanged += PropertyChangedReached;
        }
        private void PropertyChangedReached(object sender, System.ComponentModel.PropertyChangedEventArgs ev)
        {
            NotifyPropertyChanged(ev.PropertyName);
        }
        public double Rudder
        {
            get
            {
                return Info.ServerInstance.Rudder;
            }
            set { }
        }
        public double Throttle {
            get {
                return Info.ServerInstance.Throttle;
            }
            set { }
        }
      
    }
}

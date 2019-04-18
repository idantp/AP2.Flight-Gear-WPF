using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify{
        private string inputString = "";
        private ICommand clearCommand = null;
        private ICommand okCommand;
        public String BackgroundColor {
            get
            {
                if (inputString == "")
                {
                    return "White";
                }
                else
                {
                    return "Salmon";
                }
            }
        }

        public String InputString {
            set {
                this.inputString = value;
                NotifyPropertyChanged("InputString");
                NotifyPropertyChanged("BackgroundColor");

            }

            get {
                return this.inputString;
            }
        }
        public ICommand ClearCommand {
            get {
                if(clearCommand != null) { return clearCommand; }
                return clearCommand = new CommandHandler(()=>clearClicked());
            }
        }

        public ICommand OkCommand
        {
            get
            {
                if (okCommand != null) { return okCommand; }
                return okCommand = new CommandHandler(() => okClicked());
            }
        }

        private void okClicked()
        {
            string temp = this.inputString;
            inputString = "";
        }

        private void clearClicked()
        {
            InputString = "";
            NotifyPropertyChanged("InputString");
        }

    }
}

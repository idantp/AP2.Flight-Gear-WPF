using System;
using System.Windows.Input;
using FlightSimulator.Model;
using System.Threading;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify{

        public Commands mCommands;
        private string inputString;

        public AutoPilotViewModel() {
            this.mCommands = Commands.CommandsInstance;
            this.inputString = "";
        }

        // if the Auto-Pilot Text-Box is empty then paint background with White,
        // otherwise paint it with Pink
        public String BackgroundColor {
            get
            {
                return inputString == "" ? "White" : "Salmon";
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
        private ICommand clearCommand = null;

        public ICommand ClearCommand {
            get {
                if(clearCommand != null) { return clearCommand; }
                return clearCommand = new CommandHandler(()=>clearClicked());
            }
        }
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (okCommand != null) { return okCommand; }
                return okCommand = new CommandHandler(() => okClicked());
            }
        }

        // if "OK" button is clicked then clear the Commands Text-Box, then
        // apply the commands in the flight simulator.
        private void okClicked()
        {
            string temp = this.inputString;
            InputString = "";
            new Thread(() => mCommands.sendCommands(temp)).Start();

        }
        // if "Clear" button is clicked then clear the Commands Text-Box, then
        // notify a change in the Text-Box content
        private void clearClicked()
        {
            InputString = "";
            NotifyPropertyChanged("InputString");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using System.Threading;
using FlightSimulator.Views;



namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify{

        public Commands mCommands;
        private string inputString;

        public AutoPilotViewModel() {
            this.mCommands = Commands.CommandsInstance;
            this.inputString = "";
        }

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

        private void okClicked()
        {
            string temp = this.inputString;
            InputString = "";
            new Thread(() => mCommands.sendCommands(temp)).Start();

        }

        private void clearClicked()
        {
            InputString = "";
            NotifyPropertyChanged("InputString");
        }
    }
}

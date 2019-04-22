using FlightSimulator.Model;
using FlightSimulator.Views.Windows;
using FlightSimulator.Model.Interface;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        private SettingsWin settingsWindow;

        public SettingsWindowViewModel(ISettingsModel model, SettingsWin settingsWin)
        {
            this.model = model;
            this.settingsWindow = settingsWin;
        }

        // properties
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        #region Commands
        #region ClickCommand
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        // if "Save" button is clicked then save the fields in the window and close it
        private void OnClick()
        {
            model.SaveSettings();
            this.settingsWindow.Close();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        // if "Cancel" button is clicked then close the window with no changes
        private void OnCancel()
        {
            model.ReloadSettings();
            this.settingsWindow.Close();
            
        }
        #endregion
        #endregion
    }
}


using FlightSimulator.Model;
using FlightSimulator.Views.Windows;
using System.Windows.Input;
using System.Threading;

namespace FlightSimulator.ViewModels
{
    class RouteBoardViewModel: BaseNotify
    {
        private ICommand settingsCommand;
        private ICommand connectCommand;
        private ICommand disconnectCommand;


        // if "Settings" button is clicked then pop-up Settings Window
        public ICommand SettingsCommand {
            get {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => SettingsClicked()));
                
            }
        }
        private void SettingsClicked() {
            SettingsWin settingWindow = new SettingsWin();
            settingWindow.ShowDialog();
        }

        public ICommand DisconnectCommand
        {
            get
            {
                return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => DisconnectClicked()));
            }
        }

        // TODO
        private void DisconnectClicked() {
            Commands.CommandsInstance.disconnect();
            Info.ServerInstance.disconnect();
        }

        public ICommand ConnectCommand {
            get {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }

        // if "Connect" button is clicked then connect to the flight simulator as boht:
        // TCP client and TCP server
        private void OnConnect() {
            new Thread(() => {
                Info.ServerInstance.connectAsServer();
                Commands.CommandsInstance.openClientThread();
            }).Start();
            
        }
    }
}

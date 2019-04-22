using FlightSimulator.Model;
using FlightSimulator.Views.Windows;
using System.Windows.Input;
using System.Threading;

namespace FlightSimulator.ViewModels
{
    class RouteBoardViewModel: BaseNotify
    {
        private ICommand settingsCommand;
        public ICommand SettingsCommand {
            get {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnClick()));
                
            }
        }
        private void OnClick() {
            SettingsWin settingWindow = new SettingsWin();
            settingWindow.ShowDialog();
        }

        private ICommand connectCommand;
        public ICommand ConnectCommand {
            get {

                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }

        private void OnConnect() {
            new Thread(() => {
                Info.ServerInstance.connectAsServer();
                Commands.CommandsInstance.openClientThread();
            }).Start();
            
        }
    }
}

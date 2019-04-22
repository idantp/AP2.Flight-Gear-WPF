using System.Windows;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;

namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWin.xaml
    /// </summary>
    public partial class SettingsWin : Window
    {
        private SettingsWindowViewModel settingsVM;
        public SettingsWin()
        {
            InitializeComponent();
            settingsVM = new SettingsWindowViewModel(new ApplicationSettingsModel(), this);
            DataContext = settingsVM;
        }
    }
}

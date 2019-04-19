using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.ViewModels;
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

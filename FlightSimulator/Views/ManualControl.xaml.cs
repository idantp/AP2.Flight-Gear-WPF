using System.Windows.Controls;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for ManualControl.xaml
    /// </summary>
    public partial class ManualControl : UserControl
    {
        public ManualControl()
        {
            InitializeComponent();
            DataContext = new ManualControlViewModel();
        }
    }
}

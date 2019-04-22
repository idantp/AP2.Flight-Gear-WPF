using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for RouteBoard.xaml
    /// </summary>
    public partial class RouteBoard : UserControl
    {
        public RouteBoard()
        {
            InitializeComponent();
            DataContext = new RouteBoardViewModel();
        }

        private void FlightBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

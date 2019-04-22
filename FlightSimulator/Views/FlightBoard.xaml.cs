using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        private FlightBoardViewModel flightBoardVM;
        public FlightBoard()
        {
            InitializeComponent();
            flightBoardVM = new FlightBoardViewModel();
            flightBoardVM.PropertyChanged += Vm_PropertyChanged;
            DataContext = flightBoardVM;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);
            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Nullable<float> y = flightBoardVM.Lon;
            Nullable<float> x = flightBoardVM.Lat;
            // creating a new point that conssists of the current latitude and longitude
            if ((e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon")) &&
                (x!= null && y!= null))
            {
                Point p1 = new Point((float)x, (float)y);        
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}


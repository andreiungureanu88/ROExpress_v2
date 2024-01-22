using Microsoft.Maps.MapControl.WPF;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;
using ROExpress_GUI.Andrei;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ROExpress_GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for MyTrainView.xaml
    /// </summary>
    public partial class MyTrainView : UserControl
    {
        public MyTrainView()
        {
            InitializeComponent();
           
        }
        
        void drawNewRoute(List<GetCityHops_Result> hops)
        {
            myMap.Children.Clear();
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            LocationCollection stations = new LocationCollection();
            for(int i=0;i<hops.Count;i++)
            {
                stations.Add(new Location(hops[i].Latitudine, hops[i].Longitudine));
                Pushpin pin = new Pushpin();
                pin.Location = new Location(hops[i].Latitudine, hops[i].Longitudine);
                pin.Content = hops[i].Nume_Oras;
                pin.FontSize = 6;
                myMap.Children.Add(pin);
            }
            polyline.Locations = stations;
            myMap.Children.Add(polyline);
        }
        

        private void SearchTrainRouteEnterPressed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SearchTrainRoute(this, e);
            }
        }

        private void SearchTrainRoute(object sender, RoutedEventArgs e)
        {
            var existingPolylines = myMap.Children.OfType<MapPolyline>().ToList();
            foreach (var existingPolyline in existingPolylines)
            {
                myMap.Children.Remove(existingPolyline);
            }

            var TrainID = ChoosedTrainTextBox.Text;
            List<double> Xcoords = new List<double>();
            List<double> Ycoords = new List<double>();

            EntityController controller = new EntityController();
          //  LinQDatabase linQDatabase = new LinQDatabase();
            
            var searchTextBoxText = ChoosedTrainTextBox.Template.FindName("Search", ChoosedTrainTextBox) as TextBox;
            List<GetCityHops_Result> hops = controller.GetCityHopsEntity(searchTextBoxText.Text);
            drawNewRoute(hops);

            //functie de la uai
            
        }
    }
}

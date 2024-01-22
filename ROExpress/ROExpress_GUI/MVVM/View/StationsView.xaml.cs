using ROExpress_GUI.Andrei;
using ROExpress_GUI.MVVM.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ROExpress_GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for StationsView.xaml
    /// </summary>
    public partial class StationsView : UserControl
    {
        public StationsView()
        {
            InitializeComponent();
            DepartureRadioButton.IsChecked = true;
        }

        private void SearchDeparture(object sender, RoutedEventArgs e)
        {
            DepartureRadioButton.IsChecked = true;
            ListaTrenuriStatie.Items.Clear();
            at_toTextBlock.Text = "Departs at:";
            to_fromDirectionTextBlock.Text = "To direction:";
            EntityController controller = new EntityController();
           
           // LinQDatabase linQDatabase = new LinQDatabase();
            var searchTextBoxText = selectedStationTextBox.Template.FindName("Search", selectedStationTextBox) as TextBox;
            List<TrainDepartures> ATrainDepartures = controller.GetTrainDeparturesEntity(searchTextBoxText.Text);
            //citire de la uai

            
            foreach (TrainDepartures station in ATrainDepartures)
            {
                ListaTrenuriStatie.Items.Add(new Station{Hour = station.HourDepartures, Destination = station.FinalyStation, Train = station.Train});
            }
           
        }

        private void SearchArrival(object sender, RoutedEventArgs e)
        {
            ListaTrenuriStatie.Items.Clear();
            at_toTextBlock.Text = "Arrives at:";
            to_fromDirectionTextBlock.Text = "From direction:";
            //LinQDatabase linQDatabase = new LinQDatabase();
            EntityController controller = new EntityController();
            var searchTextBoxText = selectedStationTextBox.Template.FindName("Search", selectedStationTextBox) as TextBox;
            List<TrainArrivals> ATrainArrivals = controller.GetTrainArrivalsEntity(searchTextBoxText.Text);

            foreach (TrainArrivals station in ATrainArrivals)
            {
                ListaTrenuriStatie.Items.Add(new Station { Hour = station.HourArrival, Destination = station.StartStation, Train = station.Train });
            }
        }
    }
}

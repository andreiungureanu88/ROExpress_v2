using ROExpress_GUI.Andrei;
using ROExpress_GUI.Core.Bogdan;
using ROExpress_GUI.MVVM.ViewModel;
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
    /// Interaction logic for TravelView.xaml
    /// </summary>
    public partial class TravelView : UserControl
    {
        public event EventHandler ChangeContentRequested;
        string SelectedDate;
        public ICommand LoadBuyTicketUserControlCommand { get; }
        public TravelView()
        {
            InitializeComponent();
            SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            TravelDatePicker.Text = SelectedDate;
        }


        public TimeSpan CalculateTimeDifference(string startTime, string endTime)
        {

            DateTime startDateTime = DateTime.Parse(startTime);
            DateTime endDateTime = DateTime.Parse(endTime);
            TimeSpan timeDifference = endDateTime - startDateTime;

            return timeDifference;
        }

        private void FindTrain(object sender, RoutedEventArgs e)
        {
            //search for train and stuff
            int ok = 1;
            var searchDepartTextBox = LeavesTextBox.Template.FindName("Search", LeavesTextBox) as TextBox;
            var searchArrivalTextBox = ArrivesTextBox.Template.FindName("Search", ArrivesTextBox) as TextBox;
            var searchDataText = TravelDatePicker.Text.ToString();
            if(searchDepartTextBox.Text== "")
            {
                string messageBoxText = "Select Depart Station First";
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                ok = 0;
                result = MessageBox.Show(messageBoxText,caption, button, icon, MessageBoxResult.Yes);
                
            }
            if (searchArrivalTextBox.Text == "")
            {
                string messageBoxText = "Select Arrival Station First";
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                ok = 0;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            }
            if (searchDataText == "")
            {
                string messageBoxText = "Select Travel Date First";
                string caption = "WARNING";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                ok = 0;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            if (ok == 1)
            {
                //DatabaseController databaseController = new DatabaseController();
                //LinQDatabase linQDatabase = new LinQDatabase();
                EntityController controller = new EntityController();
                List<TrainJourney> trainOptions = new List<TrainJourney>();
                trainOptions = controller.GetTrainJourneysEntity(searchDepartTextBox.Text, searchArrivalTextBox.Text);
                //trainOptions = linQDatabase.GetTrainJourneysLinQ(searchDepartTextBox.Text, searchArrivalTextBox.Text);

                ListaCalatorii.Items.Clear();
                foreach (TrainJourney train in trainOptions)
                {
                    string travelTime= CalculateTimeDifference(train.DepartureTime, train.ArrivalTime).ToString();
                    ListaCalatorii.Items.Add(new Train { ID = train.IDTren, 
                                                         DepartStation = train.Departures, 
                                                         DepartTime = train.DepartureTime, 
                                                         ArriveStation = train.Arrival, 
                                                         ArriveTime = train.ArrivalTime,
                                                         TravelTime = travelTime
                                                         });
                }
            }
        }

        private void LeavesTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ArrivesTextBox.Focus();
            }
        }

        private void ArrivesTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindTrain(sender, e);
            }
        }

        private void ListaCalatorii_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ListaCalatorii.SelectedItem != null)
            {
                var searchDataText = TravelDatePicker.Text.ToString();
                DateTime date = DateTime.Parse(searchDataText);
                Console.WriteLine(date);
                contentContainer.Content = new SelectedRouteView(date, (Train)ListaCalatorii.SelectedItem);  
            }
        }
    }
}

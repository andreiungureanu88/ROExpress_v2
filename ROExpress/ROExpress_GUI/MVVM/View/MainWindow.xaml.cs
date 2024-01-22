using ROExpress_GUI.Andrei;
using ROExpress_GUI.MVVM.View;
using ROExpress_GUI.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ROExpress_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel mainViewModel;
        public MainWindow()
        {
           // DatabaseController.InitializeDataBase();
            InitializeComponent();
            mainViewModel = new MainViewModel();

          //  LinQDatabase DatabaseControllerLinQ = new LinQDatabase();

            DateTime time = new DateTime(2023, 12, 10);

            //TrainSeatAvailability  trainAvailability = DatabaseControllerLinQ.GetTrainSeatAvailability("IR1752",time);

            // pentru dezvoltator grafic format an/luna/zi
            // Atat trebuie sa faci cand vei cumpara un bilet
            // exista functia GetSeatsAvalability care iti spune cate locuri disponibile ai
            EntityController entityController = new EntityController();
            List<TrainJourney> trains = entityController.GetTrainJourneysEntity("Suceava", "Pascani");
            entityController.PrintJourneyTrainEntity(trains);

            List<GetCityHops_Result> hops = entityController.GetCityHopsEntity("IC551");
            entityController.PrintCityHops(hops);

            List<TrainArrivals> trainArrival = entityController.GetTrainArrivalsEntity("Bacau");

            List<TrainDepartures> trainDepartures = entityController.GetTrainDeparturesEntity("Bacau");

            List<RailwayStations> railwayStations = entityController.GetRailwayStationsEntity();
            DateTime dat = new DateTime(2024, 01, 27);

            //entityController.BuyTicketEntity("IC551", "andreiungureanu161@gmail.com", 2, dat, "Suceava", "Pascani", 1);

            //DatabaseControllerLinQ.BuyTicket("IC551","andreiungureanu161@gmail.com",1,dat,"Suceava", "Bucuresti Nord");
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HideApplication(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private bool isDragging = false;
        private Point startPoint;
        //implement drag and drop for the window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePosition = e.GetPosition(this);
                Point diff = (Point)(startPoint - mousePosition);
                Left -= diff.X;
                Top -= diff.Y;
            }
        }
        private void OtherWindow_ToggleFullScreenClicked(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}

using ROExpress_GUI.Andrei;
using ROExpress_GUI.Core.Bogdan;
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
    /// Interaction logic for SelectedRouteView.xaml
    /// </summary>
    public partial class SelectedRouteView : UserControl
    {
        int totalCount = 0;
        int adultsCount = 0;
        int childrenCount = 0;
        int studentsCount = 0;
        int scholarCount = 0;
        int pensionersCount = 0;
        int dogCount = 0;

        DateTime travelDate;
        Train train;
        public SelectedRouteView(DateTime travelDate, Train train)
        {
            InitializeComponent();

            this.train = train;

            tipBiletCBox.Items.Add("Standard");
            tipBiletCBox.Items.Add("Round Trip");

            clasaBiletCBox.Items.Add("1 Class");
            clasaBiletCBox.Items.Add("2 Class");

            departTBlock.Text = train.DepartStation ;
            departTBlock.Text+= " " + train.DepartTime;
            arrivalTBlock.Text = train.ArriveStation;
            arrivalTBlock.Text += " " + train.ArriveTime;
            trainIdTBlock.Text ="Train: "+ train.ID;
            TravelTimeTBlock.Text ="Travel time: "+ train.TravelTime;
            EntityController controller = new EntityController();    
            int distance = (int)controller.GetTravelDistanceEntity(train.DepartStation, train.ArriveStation);
            distanceTBlock.Text = "Distance: "+distance.ToString() + " km";
            this.travelDate = travelDate;
        }
        
        void updateSeatList()
        {
            passangerListBox.Items.Clear();
            if (adultsCount > 0)
            {
                passangerListBox.Items.Add("Adults: " + adultsCount.ToString());
            }
            if (childrenCount > 0)
            {
                passangerListBox.Items.Add("Children: " + childrenCount.ToString());
            }
            if (studentsCount > 0)
            {
                passangerListBox.Items.Add("Students: " + studentsCount.ToString());
            }
            if (scholarCount > 0)
            {
                passangerListBox.Items.Add("Scholars: " + scholarCount.ToString());
            }
            if (pensionersCount > 0)
            {
                passangerListBox.Items.Add("Pensioners: " + pensionersCount.ToString());
            }
            if (dogCount > 0)
            {
                passangerListBox.Items.Add("Dogs: " + dogCount.ToString());
            }
        }   

        private void AddSeat(object sender, RoutedEventArgs e)
        {
            String seatType = ((Button)sender).Content.ToString();
            if(seatType == "+1 Adult")
            {
                if(totalCount >= 10)
                {
                    MessageBox.Show("You can't have more than 10 passengers");
                    return;
                }
                adultsCount++;
                totalCount++;
            }
            else if (seatType == "+1 Child")
            {
                if (totalCount >= 10)
                {
                    MessageBox.Show("You can't have more than 10 passengers");
                    return;
                }
                childrenCount++;
                totalCount++;
            }
            else if (seatType == "+1 Student")
            {
                if (totalCount >= 10)
                {
                    MessageBox.Show("You can't have more than 10 passengers");
                    return;
                }
                studentsCount++;
                totalCount++;
            }
            else if (seatType == "+1 Scholar")
            {
                if (totalCount >= 10)
                {
                    MessageBox.Show("You can't have more than 10 passengers");
                    return;
                }
                scholarCount++;
                totalCount++;
            }
            else if (seatType == "+1 Pensioner")
            {
                if (totalCount >= 10)
                {
                    MessageBox.Show("You can't have more than 10 passengers");
                    return;
                }
                pensionersCount++;
                totalCount++;
            }
            else if (seatType == "+1 Dog")
            {
                if(adultsCount +pensionersCount +studentsCount < dogCount + 1)
                {
       
                    MessageBox.Show("You can't have more dogs than adults, pensioners or students","Warning");
                    return;
                }
                dogCount++;
            }
            updateSeatList();
        }

        private void chackAvailableSeats(object sender, RoutedEventArgs e)
        {
            TrainSeatAvailability trainSeatAvailability = new TrainSeatAvailability();
         //   LinQDatabase linQDatabase = new LinQDatabase();
         EntityController controller = new EntityController();
            trainSeatAvailability = controller.GetTrainSeatAvailability(train.ID, travelDate);
            MessageBox.Show("Available seats: \nFirst class: " + trainSeatAvailability.SeatsFirstClass.ToString()+"\nSecond class: "+ trainSeatAvailability.SeatsSecondClass.ToString());
            
        }

        private void buyTicket(object sender, RoutedEventArgs e)
        {
            double TotalPrice = 0;

            //LinQDatabase linQDatabase = new LinQDatabase();
            EntityController controller = new EntityController();
            int distance = (int)controller.GetTravelDistanceEntity(train.DepartStation, train.ArriveStation);
            double classFactor=0;
            double scholarFactor = 0;
            double studentFactor = 0;
            if (clasaBiletCBox.SelectedIndex == 0) //clasa 1
            {
                classFactor = 1.5;
                scholarFactor = 0.5;
                studentFactor = 0.5;

            }
            else // clasa 2
            {
                classFactor = 1;
                scholarFactor = 0;
                studentFactor = 0.1;
            }

            double typeFactor=0;
            if (tipBiletCBox.SelectedIndex == 0) //standard
            {
                typeFactor = 1;
            }
            else //round trip
            {
                typeFactor = 1.8;
            }
            double pricePerKm = 0.5;
                   
            if(distance>10)
            {
                pricePerKm = 1.2;
            }
            if(distance>50)
            {
                pricePerKm = 1;
            }
            if(distance>100)
            {
                pricePerKm = 0.8;
            }
            if(distance>200)
            {
                pricePerKm = 0.7;
            }
            if(distance>300)
            {
                pricePerKm = 0.5;
            }
            if(distance>400)
            {
                pricePerKm = 0.4;
            }
            if(distance>500)
            {
                pricePerKm = 0.3;
            }

            double trainFactor = 0;

            if (train.ID.Contains("IC"))
            {
                trainFactor = 1.4;
            }
            if (train.ID.Contains("IR"))
            {
                trainFactor = 1.2;
            }
            if (train.ID.Contains("R"))
            {
                trainFactor = 1;
            }

            

            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * adultsCount;
            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * childrenCount * 0.5;
            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * studentsCount * studentFactor;
            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * scholarCount * scholarFactor;
            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * pensionersCount * 0.8;
            TotalPrice += distance * pricePerKm * classFactor * typeFactor * trainFactor * dogCount * 0.4;

           //MessageBox.Show("Total price: " + TotalPrice.ToString() + " RON","TICKET RESUME");
            string messageBoxText = "Total price: " + TotalPrice.ToString();
            string caption = "Purchase confirmation";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon);
            if(result == MessageBoxResult.Yes)
            {
                var editWindow = new EditWindow();
                editWindow.ShowDialog();
                string email = editWindow.EditedText;
               

                controller.BuyTicketEntity(train.ID,email, clasaBiletCBox.SelectedIndex + 1,travelDate,train.DepartStation,train.ArriveStation,totalCount);

            }
            else
            {
                return;
            } 
            
        }

        private void resetSeats(object sender, RoutedEventArgs e)
        {
            totalCount = 0;
            adultsCount = 0;
            childrenCount = 0;
            studentsCount = 0;
            scholarCount = 0;
            pensionersCount = 0;
            dogCount = 0;
            updateSeatList();
        }
    }
}

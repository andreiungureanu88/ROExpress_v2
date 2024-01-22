using ROExpress_GUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ROExpress_GUI.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand TravelViewCommand { get; set; }
        public RelayCommand StationsViewCommand { get; set; }
        public RelayCommand MyTrainViewCommand { get; set; }
        public RelayCommand AccountViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public RelayCommand SelectedRouteViewCommand { get; set; }


        private object _currentView;
        public TravelViewModel TravelVm { get; set; }
        public StationsViewModel StationsVm { get; set; }
        public MyTrainViewModel MyTrainVm { get; set; }
        public AccountViewModel AccountVm { get; set; }
        public SettingsViewModel SettingsVm { get; set; }

        public SelectedRouteViewModel SelectedRouteVm { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            TravelVm = new TravelViewModel();
            StationsVm = new StationsViewModel();
            MyTrainVm = new MyTrainViewModel();
            AccountVm = new AccountViewModel();
            SettingsVm = new SettingsViewModel();
            SelectedRouteVm = new SelectedRouteViewModel();
            CurrentView = TravelVm;

            TravelViewCommand = new RelayCommand(o => {
                CurrentView = TravelVm;
            });

            StationsViewCommand = new RelayCommand(o => {
                CurrentView = StationsVm;
            });

            MyTrainViewCommand = new RelayCommand(o => {
                CurrentView = MyTrainVm;
            });

            AccountViewCommand = new RelayCommand(o => {
                CurrentView = AccountVm;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVm;
            });

            SelectedRouteViewCommand = new RelayCommand(o =>{
                CurrentView = SelectedRouteVm;
            });

        }

    }
}

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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }

        private void EditEmail_click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow();

            editWindow.ShowDialog();

            string editedText = editWindow.EditedText;

           AccountEmailTBox.Text = editedText;
        }
    }
}

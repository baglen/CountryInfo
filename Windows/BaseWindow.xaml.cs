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
using CountryInfo.Windows;
using CountryInfo.Pages;
using System.Configuration;

namespace CountryInfo.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
            Manager.baseFrame = navigationFrame;

            navigationFrame.Navigate(new ChangeConnectionStringPage(this));
        }

        private void btnSelectCountry_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new SelectCountryPage());
        }

        private void btnAllCountries_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new AllCountriesPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.GoBack();
        }

        private void navigationFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Manager.baseFrame.CanGoBack)
            {
                btnBack.IsEnabled = true;
                btnBack.Visibility = Visibility.Visible;

            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
                
        }

        private void btnChangeConnString_Click(object sender, RoutedEventArgs e)
        {
            Manager.baseFrame.Navigate(new ChangeConnectionStringPage(this));
        }
    }
}

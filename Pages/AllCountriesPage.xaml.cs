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
using CountryInfo.Model;

namespace CountryInfo.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllCountriesPage.xaml
    /// </summary>
    public partial class AllCountriesPage : Page
    {
        public AllCountriesPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           try
           {
                if (Visibility == Visibility.Visible)
                {
                    dGridCountries.ItemsSource = CountriesEntities.GetContext().Country.ToList();
                }
           }
           catch
           {
                MessageBox.Show("Ошибка обновления данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
           }
        }
        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Country> currentCountry = CountriesEntities.GetContext().Country.ToList();
                currentCountry = currentCountry.Where(p => p.Title.ToLower().Contains(txtBoxSearch.Text.ToLower()) || 
                p.CountryCode.ToLower().ToString().Contains(txtBoxSearch.Text.ToLower()) || p.City.Title.ToLower().Contains(txtBoxSearch.Text.ToLower())
                || p.Square.ToString().ToLower().Contains(txtBoxSearch.Text.ToLower()) || p.Population.ToString().ToLower().Contains(txtBoxSearch.Text.ToLower())
                || p.Region.Title.ToString().ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
                dGridCountries.ItemsSource = currentCountry.ToList();
                if (currentCountry.Count == 0)
                {
                    lblNothingFound.Visibility = Visibility.Visible;
                }
                else
                {
                    lblNothingFound.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка обновления данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

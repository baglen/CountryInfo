using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddEditCountryPage.xaml
    /// </summary>
    public partial class AddEditCountryPage : Page
    {
        private Manager.Country currentCountry;
        Country countryToAdd = new Country();
        public AddEditCountryPage(Manager.Country selectedCountry)
        {
            InitializeComponent();
            currentCountry = new Manager.Country();
            currentCountry = selectedCountry;
            DataContext = currentCountry;
            try
            {
                if (CountriesEntities.GetContext().Country.Where(p => p.Title == currentCountry.Title).Count() != 0)
                {
                    countryToAdd = CountriesEntities.GetContext().Country.Where(p => p.Title == currentCountry.Title).First();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка обновления данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (String.IsNullOrEmpty(currentCountry.Title))
                stringBuilder.AppendLine("Не введено название страны");
            if (String.IsNullOrEmpty(currentCountry.CountryCode))
                stringBuilder.AppendLine("Не введен код страны");
            if (String.IsNullOrEmpty(currentCountry.Capital))
                stringBuilder.AppendLine("Не введена площадь страны");
            if (String.IsNullOrEmpty(txtBoxSquare.Text))
                stringBuilder.AppendLine("Не введена столица страны");
            if (String.IsNullOrEmpty(txtBoxPopulation.Text))
                stringBuilder.AppendLine("Не введено население страны");
            if (String.IsNullOrEmpty(currentCountry.Region))
                stringBuilder.AppendLine("Не введен регион страны");
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                try
                {
                    City currentCapital = new City();
                    Region currentRegion = new Region();
                    if (CountriesEntities.GetContext().City.Where(p => p.Title == txtBoxCapital.Text.Trim()).Count() == 0)
                    {
                        currentCapital.Title = txtBoxCapital.Text.Trim();
                        CountriesEntities.GetContext().City.Add(currentCapital);
                        countryToAdd.Capital = currentCapital.Id;
                    }
                    else
                    {
                        countryToAdd.Capital = CountriesEntities.GetContext().City.Where(p => p.Title == txtBoxCapital.Text).Select(p => p.Id).FirstOrDefault();
                    }
                    if (CountriesEntities.GetContext().Region.Where(p => p.Title == txtBoxRegion.Text.Trim()).Count() == 0)
                    {
                        currentRegion.Title = txtBoxRegion.Text.Trim();
                        CountriesEntities.GetContext().Region.Add(currentRegion);
                        countryToAdd.RegionId = currentRegion.Id;
                    }
                    else
                    {
                        countryToAdd.RegionId = CountriesEntities.GetContext().Region.Where(p => p.Title == txtBoxRegion.Text).Select(p => p.Id).FirstOrDefault();
                    }
                    if (CountriesEntities.GetContext().Country.Where(p => p.CountryCode == txtBoxCode.Text.Trim()).Count() == 0)
                    {
                        CountriesEntities.GetContext().Country.Add(countryToAdd);
                    }
                    countryToAdd.Square = decimal.Parse(txtBoxSquare.Text, CultureInfo.InvariantCulture);
                    countryToAdd.Title = txtBoxTitle.Text;
                    countryToAdd.CountryCode = txtBoxCode.Text;
                    countryToAdd.Population = Convert.ToInt32(txtBoxPopulation.Text);
                    CountriesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.baseFrame.GoBack();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        sb.AppendLine("Object: " + validationError.Entry.Entity.ToString());
                        sb.AppendLine("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            sb.AppendLine(err.ErrorMessage + "");
                        }
                    }
                    MessageBox.Show(sb.ToString());
                }
            }
        }

        private void txtBoxSquare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void txtBoxPopulation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

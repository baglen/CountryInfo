using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для SelectCountryPage.xaml
    /// </summary>
    public partial class SelectCountryPage : Page
    {
        private Manager.Country selectedCountry = new Manager.Country();
        public SelectCountryPage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(txtBoxSearch.Text))
            {
                loaderGif.Visibility = Visibility.Visible;
                GetDataAsync(txtBoxSearch.Text);
            }
            else
            {
                MessageBox.Show("Название страны не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void GetDataAsync(string country)
        {
            try
            {
                Uri page = new Uri("https://restcountries.eu/rest/v2/name/"+country);
                HttpClient client = new HttpClient();
                try 
                {
                    string response = await client.GetStringAsync(page);
                    JArray objectArray = (JArray)JsonConvert.DeserializeObject(response);
                    foreach (JObject jobject in objectArray)
                    {
                        selectedCountry.Title = jobject["name"].ToString();
                        selectedCountry.CountryCode = jobject["alpha2Code"].ToString();
                        selectedCountry.Capital = jobject["capital"].ToString();
                        selectedCountry.Square = Convert.ToDecimal(jobject["area"].ToString());
                        selectedCountry.Population = Convert.ToInt32(jobject["population"]);
                        selectedCountry.Region = jobject["region"].ToString();
                    }
                    loaderGif.Visibility = Visibility.Hidden;
                    DataContext = null;
                    DataContext = selectedCountry;
                    gridTitles.Visibility = Visibility.Visible;
                    if (MessageBox.Show("Желаете добавить данную страну в БД?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        Manager.baseFrame.Navigate(new AddEditCountryPage(selectedCountry));
                }
                catch
                {
                    MessageBox.Show("Страна с таким названием не найдена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    loaderGif.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка связи с API сервисом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                loaderGif.Visibility = Visibility.Hidden;
            }
        }
    }
}

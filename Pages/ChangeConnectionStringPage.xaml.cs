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
using System.Configuration;
using System.Xml;
using CountryInfo.Windows;
using System.Web;

namespace CountryInfo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeConnectionStringPage.xaml
    /// </summary>
    public partial class ChangeConnectionStringPage : Page
    {
        private BaseWindow parentWindow;
        public ChangeConnectionStringPage(BaseWindow currentBaseWindow)
        {
            InitializeComponent();
            parentWindow = currentBaseWindow;
            radioWindows.IsChecked = true;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {

                if(radioWindows.IsChecked == true)
                { 
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    foreach (XmlElement element in xmlDoc.DocumentElement)
                    {
                        if (element.Name.Equals("connectionStrings"))
                        {
                            foreach (XmlNode node in element.ChildNodes)
                            {
                                if (node.Attributes[0].Value.Equals("CountriesEntities"))
                                {
                                    node.Attributes[1].Value = $"metadata=res://*/Model.BaseModel.csdl|res://*/Model.BaseModel.ssdl|res://*/Model.BaseModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={txtBoxDataSourceWindows.Text};initial catalog=Countries;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\"";
                                }
                            }
                        }
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    ConfigurationManager.RefreshSection("connectionStrings");
                    if (MessageBox.Show("Для дальнейшей работы запустите приложение снова", "Информация", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        parentWindow.Close();
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    foreach (XmlElement element in xmlDoc.DocumentElement)
                    {
                        if (element.Name.Equals("connectionStrings"))
                        {
                            foreach (XmlNode node in element.ChildNodes)
                            {
                                if (node.Attributes[0].Value.Equals("CountriesEntities"))
                                {
                                    node.Attributes[1].Value = $"metadata=res://*/Model.BaseModel.csdl|res://*/Model.BaseModel.ssdl|res://*/Model.BaseModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={txtBoxDataSourceSql.Text};initial catalog=Countries; user id={txtBoxDataLoginSql.Text}; password={passBoxDataPassSql.Password}; MultipleActiveResultSets = True; App = EntityFramework\"";
                                }
                            }
                        }
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    ConfigurationManager.RefreshSection("connectionStrings");
                    if (MessageBox.Show("Для дальнейшей работы запустите приложение снова", "Информация", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        parentWindow.Close();
                }
        }

        private void radioWindows_Checked(object sender, RoutedEventArgs e)
        {
            stackWindows.Visibility = Visibility.Visible;
            stackSql.Visibility = Visibility.Collapsed;
        }

        private void radioSql_Checked(object sender, RoutedEventArgs e)
        {
            stackSql.Visibility = Visibility.Visible;
            stackWindows.Visibility = Visibility.Collapsed;
        }
    }
}

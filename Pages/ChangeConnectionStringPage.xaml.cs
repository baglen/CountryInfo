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
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
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
                            node.Attributes[1].Value = $"metadata=res://*/Model.BaseModel.csdl|res://*/Model.BaseModel.ssdl|res://*/Model.BaseModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source={txtBoxDataSource.Text};initial catalog=Countries;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.EntityClient\"";
                        }
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("connectionStrings");
            BaseWindow baseWindow = new BaseWindow();
            baseWindow.Show();
            parentWindow.Close();        
        }

        private void btnShowConnectionString_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ConfigurationManager.ConnectionStrings["CountriesEntities"].ConnectionString);
        }
    }
}

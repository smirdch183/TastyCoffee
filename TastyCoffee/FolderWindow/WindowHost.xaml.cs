using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TastyCoffee.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowHost.xaml
    /// </summary>
    public partial class WindowHost : Window
    {
        SqlConnectionStringBuilder stringBuilder;
        public WindowHost()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            servTB.Text = stringBuilder.DataSource;
            dbTB.Text = stringBuilder.InitialCatalog;
        }

        private void BnConection_Click(object sender, RoutedEventArgs e)
        {
            if (servTB.Text != "" && dbTB.Text != "")
            {
                stringBuilder.DataSource = servTB.Text;
                stringBuilder.InitialCatalog = dbTB.Text;
                stringBuilder.IntegratedSecurity = true;
                var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                var conStrSect = (ConnectionStringsSection)config.GetSection("connectionStrings");
                conStrSect.ConnectionStrings["TastyCoffee.Properties.Settings.TastyCoffeeConnectionString"].ConnectionString = stringBuilder.ConnectionString;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void BnContion_Click(object sender, RoutedEventArgs e)
        {
            WindowAvtorization windowAvtorization = new WindowAvtorization();
            windowAvtorization.Show();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace TastyCoffee.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowMenu.xaml
    /// </summary>
    public partial class WindowMenu : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public string Role;
        public string ID;
        public string Login;
        public string Password;
        public string LastName;
        public string FirstName;
        SqlConnectionStringBuilder stringBuilder;
        public WindowMenu(string login, string password)
        {
            InitializeComponent();
            BnClients.Visibility = Visibility.Collapsed;
            BnMyZakaz.Visibility = Visibility.Collapsed;
            BnZakaz.Visibility = Visibility.Collapsed;
            BnTovar.Visibility = Visibility.Collapsed;
            BnSotrudnik.Visibility = Visibility.Collapsed;
            BnZakazDelivery.Visibility = Visibility.Collapsed;
            BnDeliveryMyZakaz.Visibility = Visibility.Collapsed;

            Login = login;
            Password = password;

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);
            try
            {
                cmd.CommandText = "Select * from Sotrudnik "
                                + "where LoginSotrudnik = '" + login + "' "
                                + "and PasswordSotrudnik = '" + password + "'";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                    Role = reader["IdRole"].ToString();
                    ID = reader["IdSotrudnik"].ToString();
                    LastName = reader["LastNameSotrudnik"].ToString();
                    FirstName = reader["FirstNameSotrudnik"].ToString();
                }
                if (i == 1)
                {
                    LbName.Content = LastName +" "+ FirstName;
                    if (Role == "1")
                    {
                        BnClients.Visibility = Visibility.Visible;
                        BnTovar.Visibility = Visibility.Visible;
                        BnSotrudnik.Visibility = Visibility.Visible;
                    }
                    else if (Role == "3")
                    {
                        BnDeliveryMyZakaz.Visibility = Visibility.Visible;
                        BnZakazDelivery.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    connection.Close();
                    cmd.CommandText = "Select * from Clients "
                                + "where LoginClient = '" + login + "' "
                                + "and PasswordClient = '" + password + "'";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int k = 0;
                    while (reader.Read())
                    {
                        k++;
                        ID = reader["IdClient"].ToString();
                        LastName = reader["LastNameClient"].ToString();
                        FirstName = reader["FirstNameClient"].ToString();
                    }
                    if (k == 1)
                    {
                        LbName.Content = LastName +" "+ FirstName;
                        BnMyZakaz.Visibility = Visibility.Visible;
                        BnZakaz.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catch Block = " + ex);
            }
            connection.Close();
        }

        private void BnExit_Click(object sender, RoutedEventArgs e)
        {
            WindowAvtorization MW = new WindowAvtorization();
            MW.Show();
            this.Close();
        }

        private void BnZakaz_Click(object sender, RoutedEventArgs e)
        {
            new WindowUserZakaz(ID).ShowDialog();
        }

        private void BnMyZakaz_Click(object sender, RoutedEventArgs e)
        {
            new WindowUserMyZakaz(ID).ShowDialog();
        }

        private void BnSotrudnik_Click(object sender, RoutedEventArgs e)
        {
            new WindowAdminSotr().ShowDialog();
        }

        private void BnClients_Click(object sender, RoutedEventArgs e)
        {
            new WindowAdminClient().ShowDialog();
        }

        private void BnTovar_Click(object sender, RoutedEventArgs e)
        {
            new WindowAdmnSelectionOfProducts().ShowDialog();
        }

        private void BnInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation windowInformation = new WindowInformation(Login, Password);
            windowInformation.Show();
            this.Close();
        }

        private void BnZakazDelivery_Click(object sender, RoutedEventArgs e)
        {
            new WindowDeliveryZakaz(ID).ShowDialog();
        }

        private void BnDeliveryMyZakaz_Click(object sender, RoutedEventArgs e)
        {
            new WindowDeliveryMyZakaz(ID).ShowDialog();
        }
    }
}

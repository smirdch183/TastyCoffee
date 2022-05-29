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
    /// Логика взаимодействия для WindowInformation.xaml
    /// </summary>
    public partial class WindowInformation : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public string Login;
        public string Password;
        public string Role;
        public WindowInformation(string login, string password)
        {
            InitializeComponent();
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
                }
                if (i == 1)
                {
                    if (Role != "1")
                    {
                        BnPassword.Visibility = Visibility.Collapsed;
                        BnEdit.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catch Block = " + ex);
            }
            connection.Close();
        }

        private void BnPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowPasswordEdit windowPasswordEdit = new WindowPasswordEdit(Login, Password);
            windowPasswordEdit.Show();
            this.Close();
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowInfoEdit windowInfoEdit = new WindowInfoEdit(Login, Password);
            windowInfoEdit.Show();
            this.Close();
        }

        private void BnInfo_Click(object sender, RoutedEventArgs e)
        {
            new WindowInfo(Login, Password).Show();
        }

        private void BnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowMenu windowMenu = new WindowMenu(Login, Password);
            windowMenu.Show();
            this.Close();
        }
    }
}

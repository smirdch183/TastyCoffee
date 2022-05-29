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
    /// Логика взаимодействия для WindowInfo.xaml
    /// </summary>
    public partial class WindowInfo : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public string Role;
        public string Login;
        public string LastName;
        public string FirstName;
        public string MidleName;
        public string Birthday;
        public string Phone;
        public string Email;
        SqlConnectionStringBuilder stringBuilder;
        public WindowInfo(string login, string password)
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

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
                Login = login;
                LastName = reader["LastNameSotrudnik"].ToString();
                FirstName = reader["FirstNameSotrudnik"].ToString();
                MidleName = reader["MidleNameSotrudnik"].ToString();
                Birthday = reader["BirthdaySotrudnik"].ToString();
                Phone = reader["Phone"].ToString();
            }
            if (i == 1)
            {
                if (Role == "1")
                {
                    LbUser.Content = "Администратор";
                }
                else if (Role == "3")
                {
                    LbUser.Content = "Доставщик";
                }
                LbEmail.Visibility = Visibility.Collapsed;
                LbMail.Visibility = Visibility.Collapsed;
                LbDate.Content = Birthday;
                LbEmail.Content = "-";
                LbFirstName.Content = FirstName;
                LbLastName.Content = LastName;
                LbLogin.Content = Login;
                LbMidleName.Content = MidleName;
                LbPhone.Content = Phone;
            }
            else
            {
                LbUser.Content = "Клиент";
                connection.Close();
                cmd.CommandText = "Select * from Clients "
                            + "where LoginClient = '" + login + "' "
                            + "and PasswordClient= '" + password + "'";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                int k = 0;
                while (reader.Read())
                {
                    k++;
                    Login = login;
                    LastName = reader["LastNameClient"].ToString();
                    FirstName = reader["FirstNameClient"].ToString();
                    MidleName = reader["MidleNameClient"].ToString();
                    Birthday = reader["BirthdayClient"].ToString();
                    Phone = reader["PhoneClient"].ToString();
                    Email = reader["EmailClient"].ToString();
                }
                if (k == 1)
                {
                    LbDate.Content = Birthday;
                    LbEmail.Content = Email;
                    LbFirstName.Content = FirstName;
                    LbLastName.Content = LastName;
                    LbLogin.Content = Login;
                    LbMidleName.Content = MidleName;
                    LbPhone.Content= Phone;
                    connection.Close();
                }
            }
            connection.Close();
        }
    }
}

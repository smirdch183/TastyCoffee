using System;
using System.Collections.Generic;
using System.Data;
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
using TastyCoffee.FolderData;
using TastyCoffee.FolderData.DannieTableAdapters;

namespace TastyCoffee.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowInfoEdit.xaml
    /// </summary>
    public partial class WindowInfoEdit : Window
    {
        Dannie dataSet;
        ClientsTableAdapter clientsTableAdapter;
        SotrudnikTableAdapter sotrudnikTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public string Role;
        SqlConnectionStringBuilder stringBuilder;

        public string LoginUser;
        public string LastNameUser;
        public string FirstNameUser;
        public string MidleNameUser;
        public string BirthdayUser;
        public string PhoneUser;
        public string EmailUser;
        public string LoginSotrudnik;
        public string LastNameSotrudnik;
        public string FirstNameSotrudnik;
        public string MidleNameSotrudnik;
        public string BirthdaySotrudnik;
        public string Phone;
        public string Login;
        public string Password;
        public string ID;

        public WindowInfoEdit(string login, string password)
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            sotrudnikTableAdapter = new SotrudnikTableAdapter();
            clientsTableAdapter = new ClientsTableAdapter();

            sotrudnikTableAdapter.Fill(dataSet.Sotrudnik);
            clientsTableAdapter.Fill(dataSet.Clients);

            Login = login;
            Password = password;

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
                LoginSotrudnik = login;
                LastNameSotrudnik = reader["LastNameSotrudnik"].ToString();
                FirstNameSotrudnik = reader["FirstNameSotrudnik"].ToString();
                MidleNameSotrudnik = reader["MidleNameSotrudnik"].ToString();
                BirthdaySotrudnik = reader["BirthdaySotrudnik"].ToString();
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
                TbEmail.Visibility = Visibility.Collapsed;
                TbMail.Visibility = Visibility.Collapsed;
                DpData.Text = BirthdaySotrudnik;
                TbEmail.Text = "-";
                TbFirstName.Text = FirstNameSotrudnik;
                TbLastName.Text = LastNameSotrudnik;
                TbLogin.Text = LoginSotrudnik;
                TbMidleName.Text = MidleNameSotrudnik;
                TbPhone.Text = Phone;
            }
            else
            {
                LbUser.Content = "Клиент";
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
                    LoginUser = login;
                    LastNameUser = reader["LastNameClient"].ToString();
                    FirstNameUser = reader["FirstNameClient"].ToString();
                    MidleNameUser = reader["MidleNameClient"].ToString();
                    BirthdayUser = reader["BirthdayClient"].ToString();
                    PhoneUser = reader["PhoneClient"].ToString();
                    EmailUser = reader["EmailClient"].ToString();
                }
                if (k == 1)
                {
                    DpData.Text = BirthdayUser;
                    TbEmail.Text = EmailUser;
                    TbFirstName.Text = FirstNameUser;
                    TbLastName.Text = LastNameUser;
                    TbLogin.Text = LoginUser;
                    TbMidleName.Text = MidleNameUser;
                    TbPhone.Text = PhoneUser;
                    connection.Close();
                }
            }
            connection.Close();
        }

        private void BnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DpData.Text != "" && TbFirstName.Text != "" && TbLastName.Text != "" && TbLogin.Text != "" && TbPhone.Text != "")
            {
                try
                {
                    connection.Close();
                    cmd.CommandText = "Select * from Sotrudnik "
                                    + "where LoginSotrudnik = '" + Login + "' "
                                    + "and PasswordSotrudnik = '" + Password + "'";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                        ID = reader["IdSotrudnik"].ToString();
                    }
                    if (i == 1)
                    {
                        sotrudnikTableAdapter.UpdateQuery(TbLogin.Text, Password, TbLastName.Text,
                            TbFirstName.Text, TbMidleName.Text, DpData.Text, TbPhone.Text,
                            Convert.ToInt32(Role), Convert.ToInt32(ID));
                        connection.Close();
                        MessageBox.Show("Изменения сохранены");
                        WindowInformation windowInformation = new WindowInformation(Login, Password);
                        windowInformation.Show();
                        this.Close();
                    }
                    else
                    {
                        connection.Close();
                        cmd.CommandText = "Select * from Clients "
                                    + "where LoginClient = '" + Login + "' "
                                    + "and PasswordClient = '" + Password + "'";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        int k = 0;
                        while (reader.Read())
                        {
                            k++;
                            ID = reader["IdClient"].ToString();
                        }
                        if (k == 1)
                        {
                            clientsTableAdapter.UpdateQuery(TbLogin.Text, Password, TbLastName.Text,
                            TbFirstName.Text, TbMidleName.Text, DpData.Text, TbPhone.Text,
                            TbEmail.Text, Convert.ToInt32(ID));
                            connection.Close();
                            MessageBox.Show("Изменения сохранены");
                            WindowInformation windowInformation = new WindowInformation(Login, Password);
                            windowInformation.Show();
                            this.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Catch Block = " + ex);
                }
            }
            else
            {
                connection.Close();
                MessageBox.Show("Заполните все поля");
            }
        }

        private void BnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation windowInformation = new WindowInformation(Login, Password);
            windowInformation.Show();
            this.Close();
        }
    }
}

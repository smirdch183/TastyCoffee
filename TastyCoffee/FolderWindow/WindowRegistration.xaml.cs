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
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        Dannie dataSet;
        ClientsTableAdapter clientsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;

        public WindowRegistration()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            clientsTableAdapter = new ClientsTableAdapter();

            clientsTableAdapter.Fill(dataSet.Clients);
        }

        private void BnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowAvtorization windowAvtorization = new WindowAvtorization();
            windowAvtorization.Show();
            this.Close();
        }

        private void BnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (DpBithday.Text != "" && TbFirstName.Text != "" && TbLastName.Text != "" && TbLogin.Text != "" && TbPhone.Text != "" && PbPassword.Password != "")
            {
                try
                {
                    connection.Close();
                    cmd.CommandText = "Select * from Clients "
                                       + "where LoginClient = '" + TbLogin.Text + "' ";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                    }
                    if (i == 0)
                    {
                        clientsTableAdapter.Insert(TbLogin.Text, PbPassword.Password, TbLastName.Text,
                        TbFirstName.Text, TbMidleName.Text, DpBithday.Text, TbPhone.Text,
                        TbEmail.Text);
                        connection.Close();
                        MessageBox.Show("Пользователь создан");
                        WindowAvtorization windowAvtorization = new WindowAvtorization();
                        windowAvtorization.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином уже есть");
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
    }
}

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
    /// Логика взаимодействия для WindowPasswordEdit.xaml
    /// </summary>
    public partial class WindowPasswordEdit : Window
    {
        Dannie dataSet;
        ClientsTableAdapter clientsTableAdapter;
        SotrudnikTableAdapter sotrudnikTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public string ID;
        SqlConnectionStringBuilder stringBuilder;

        public string Login;
        public string Password;
        public WindowPasswordEdit(string login, string password)
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
        }

        private void BnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowInformation windowInformation = new WindowInformation(Login, Password);
            windowInformation.Show();
            this.Close();
        }

        private void BnSave_Click(object sender, RoutedEventArgs e)
        {
            if (PbStarPass.Password == "" || PbNewPass.Text == "" || PbNewPovPass.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
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
                        if (PbNewPass.Text == PbNewPovPass.Text && PbStarPass.Password == Password && PbStarPass.Password != PbNewPass.Text)
                        {
                            sotrudnikTableAdapter.UpdateSotrPass(PbNewPass.Text, Convert.ToInt32(ID));
                            connection.Close();
                            MessageBox.Show("Пароль сохранён. Перезайдите в систему!");
                            WindowAvtorization windowAvtorization = new WindowAvtorization();
                            windowAvtorization.Show();
                            this.Close();
                        }
                        else
                        {
                            connection.Close();
                            MessageBox.Show("Ошибка изменения");
                        }
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
                            if (PbNewPass.Text == PbNewPovPass.Text && PbStarPass.Password == Password && PbStarPass.Password != PbNewPass.Text)
                            {
                                clientsTableAdapter.UpdateClientPass(PbNewPass.Text, Convert.ToInt32(ID));
                                connection.Close();
                                MessageBox.Show("Пароль сохранён. Перезайдите в систему!");
                                WindowAvtorization windowAvtorization = new WindowAvtorization();
                                windowAvtorization.Show();
                                this.Close();
                            }
                            else
                            {
                                connection.Close();
                                MessageBox.Show("Ошибка изменения");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Catch Block = " + ex);
                }
            }
        }
    }
}

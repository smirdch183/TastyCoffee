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
    public partial class WindowAvtorization : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowAvtorization()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        private void BnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && PbPassword.Password != "")
            {
                try
                {
                    cmd.CommandText = "Select * from Sotrudnik "
                                    + "where LoginSotrudnik = '" + TbLogin.Text + "' "
                                    + "and PasswordSotrudnik = '" + PbPassword.Password + "'";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                    }
                    if (i == 1)
                    {
                        connection.Close();
                        WindowMenu win = new WindowMenu(TbLogin.Text, PbPassword.Password);
                        win.Show();
                        this.Close();
                    }
                    else
                    {
                        connection.Close();
                        cmd.CommandText = "Select * from Clients "
                                    + "where LoginClient = '" + TbLogin.Text + "' "
                                    + "and PasswordClient = '" + PbPassword.Password + "'";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        int k = 0;
                        while (reader.Read())
                        {
                            k++;
                        }
                        if (k == 1)
                        {
                            connection.Close();
                            WindowMenu win = new WindowMenu(TbLogin.Text, PbPassword.Password);
                            win.Show();
                            this.Close();
                        }
                        else
                        {
                            connection.Close();
                            MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации");
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
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void BnReg_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration windowRegistration = new WindowRegistration();
            windowRegistration.Show();
            this.Close();
        }
    }
}

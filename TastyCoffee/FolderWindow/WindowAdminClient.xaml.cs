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
    /// Логика взаимодействия для WindowAdminClient.xaml
    /// </summary>
    public partial class WindowAdminClient : Window
    {
        Dannie dataSet;
        View_ClientsTableAdapter view_ClientsTableAdapter;
        ClientsTableAdapter clientsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowAdminClient()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_ClientsTableAdapter = new View_ClientsTableAdapter();
            clientsTableAdapter = new ClientsTableAdapter();

            view_ClientsTableAdapter.Fill(dataSet.View_Clients);
            clientsTableAdapter.Fill(dataSet.Clients);

            DgClient.ItemsSource = dataSet.View_Clients.DefaultView;
            DgClient.SelectionMode = DataGridSelectionMode.Single;
            DgClient.SelectedValuePath = "IdClient";
            DgClient.CanUserAddRows = false;
            DgClient.CanUserDeleteRows = false;
            DgClient.IsReadOnly = true;
        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "")
            {
                connection.Close();
                cmd.CommandText = "Select * from Clients "
                                   + "where LoginClient= '" + TbLogin.Text + "' ";
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
                    clientsTableAdapter.Insert(TbLogin.Text, TbPassword.Text, TbLastName.Text, TbFirstName.Text, TbMidleName.Text, DpBithday.Text, TbPhone.Text, TbEmail.Text);
                    view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                    connection.Close();
                    MessageBox.Show("Клиент создан");
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Клиент с таким логином уже есть");
                }
            }
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "")
            {
                clientsTableAdapter.UpdateQuery(TbLogin.Text, TbPassword.Text, TbLastName.Text, TbFirstName.Text, TbMidleName.Text, DpBithday.Text, TbPhone.Text, TbEmail.Text, (int)DgClient.SelectedValue);
                view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                connection.Close();
                MessageBox.Show("Сотрудник изменён");
            }
        }

        private void BnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "")
            {
                clientsTableAdapter.DeleteAll((int)DgClient.SelectedValue);
                view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                MessageBox.Show("Сотрудник удален");
            }
        }

        private void DgClient_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DgClient.SelectedItem;
            if (dataRowView != null)
            {
                TbLogin.Text = dataRowView.Row.Field<String>("Логин");
                TbPassword.Text = dataRowView.Row.Field<String>("Пароль");
                TbLastName.Text = dataRowView.Row.Field<String>("Фамилия");
                TbFirstName.Text = dataRowView.Row.Field<String>("Имя");
                TbMidleName.Text = dataRowView.Row.Field<String>("Отчество");
                TbPhone.Text = dataRowView.Row.Field<String>("Телефон");
                DpBithday.Text = dataRowView.Row.Field<String>("Дата рождения");
                TbEmail.Text = dataRowView.Row.Field<String>("Почта");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgClient.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}

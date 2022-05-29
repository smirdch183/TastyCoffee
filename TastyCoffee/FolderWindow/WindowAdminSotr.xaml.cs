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
    /// Логика взаимодействия для WindowAdminSotr.xaml
    /// </summary>
    public partial class WindowAdminSotr : Window
    {
        Dannie dataSet;
        View_Role_ComboBoxTableAdapter view_Role_ComboBoxTableAdapter;
        View_SotrudnikTableAdapter view_SotrudnikTableAdapter;
        SotrudnikTableAdapter sotrudnikTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowAdminSotr()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_Role_ComboBoxTableAdapter = new View_Role_ComboBoxTableAdapter();
            view_SotrudnikTableAdapter = new View_SotrudnikTableAdapter();
            sotrudnikTableAdapter = new SotrudnikTableAdapter();

            view_Role_ComboBoxTableAdapter.Fill(dataSet.View_Role_ComboBox);
            view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
            sotrudnikTableAdapter.Fill(dataSet.Sotrudnik);

            CbRole.ItemsSource = dataSet.View_Role_ComboBox.DefaultView;
            CbRole.DisplayMemberPath = "Роль";
            CbRole.SelectedValuePath = "IdRole";
            CbRole.SelectedIndex = 1;

            DgSotr.ItemsSource = dataSet.View_Sotrudnik.DefaultView;
            DgSotr.SelectionMode = DataGridSelectionMode.Single;
            DgSotr.SelectedValuePath = "IdSotrudnik";
            DgSotr.CanUserAddRows = false;
            DgSotr.CanUserDeleteRows = false;
            DgSotr.IsReadOnly = true;
        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CbRole.Text != "" && TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "")
            {
                connection.Close();
                cmd.CommandText = "Select * from Sotrudnik "
                                   + "where LoginSotrudnik= '" + TbLogin.Text + "' ";
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
                    sotrudnikTableAdapter.Insert(TbLogin.Text, TbPassword.Text, TbLastName.Text, TbFirstName.Text, TbMidleName.Text, DpBithday.Text, TbPhone.Text, (int)CbRole.SelectedValue);
                    view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                    connection.Close();
                    MessageBox.Show("Сотрудник создан");
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Сотрудник с таким логином уже есть");
                }
            }
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CbRole.Text != "" && TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "" && (DgSotr.SelectedValue).ToString() != "1")
            {
                    sotrudnikTableAdapter.UpdateQuery(TbLogin.Text, TbPassword.Text, TbLastName.Text, TbFirstName.Text, TbMidleName.Text, DpBithday.Text, TbPhone.Text, (int)CbRole.SelectedValue, (int)DgSotr.SelectedValue);
                    view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                    connection.Close();
                    MessageBox.Show("Сотрудник изменён");
            }
        }

        private void BnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (CbRole.Text != "" && TbLogin.Text != "" && TbPassword.Text != "" && TbLastName.Text != "" && TbFirstName.Text != "" && DpBithday.Text != "" && TbPhone.Text != "" && (DgSotr.SelectedValue).ToString() != "1")
            {
                sotrudnikTableAdapter.DeleteAll((int)DgSotr.SelectedValue);
                view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                MessageBox.Show("Сотрудник удален");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgSotr.Columns[0].Visibility = Visibility.Hidden;
            DgSotr.Columns[1].Visibility = Visibility.Hidden;
        }

        private void DgSotr_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DgSotr.SelectedItem;
            if (dataRowView != null)
            {
                TbLogin.Text = dataRowView.Row.Field<String>("Логин");
                TbPassword.Text = dataRowView.Row.Field<String>("Пароль");
                TbLastName.Text = dataRowView.Row.Field<String>("Фамилия");
                TbFirstName.Text = dataRowView.Row.Field<String>("Имя");
                TbMidleName.Text = dataRowView.Row.Field<String>("Отчество");
                TbPhone.Text = dataRowView.Row.Field<String>("Телефон");
                DpBithday.Text = dataRowView.Row.Field<String>("Дата рождения");
                CbRole.SelectedValue = dataRowView.Row.Field<int>("IdRole");
            }
        }
    }
}

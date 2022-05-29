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
    /// Логика взаимодействия для WindowAdminAddTae.xaml
    /// </summary>
    public partial class WindowAdminAddTae : Window
    {
        Dannie dataSet;
        View_TeaTableAdapter view_TeaTableAdapter;
        TeaTableAdapter teaTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowAdminAddTae()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_TeaTableAdapter = new View_TeaTableAdapter();
            teaTableAdapter = new TeaTableAdapter();

            view_TeaTableAdapter.Fill(dataSet.View_Tea);
            teaTableAdapter.Fill(dataSet.Tea);

            DgTea.ItemsSource = dataSet.View_Tea.DefaultView;
            DgTea.SelectionMode = DataGridSelectionMode.Single;
            DgTea.SelectedValuePath = "IdTea";
            DgTea.CanUserAddRows = false;
            DgTea.CanUserDeleteRows = false;
            DgTea.IsReadOnly = true;
        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text != "" && TbPrice.Text != "" && TbColvo.Text != "" && TbColvo.Text != "0" && TbPrice.Text != "0")
            {
                connection.Close();
                cmd.CommandText = "Select * from Tea "
                                   + "where Name= '" + TbName.Text + "' ";
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
                    teaTableAdapter.Insert(TbName.Text, TbPrice.Text, Convert.ToInt32(TbColvo.Text));
                    view_TeaTableAdapter.Fill(dataSet.View_Tea);
                    connection.Close();
                    MessageBox.Show("Чай создано");
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Такое чай уже есть");
                }
            }
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text != "" && TbPrice.Text != "" && TbColvo.Text != "" && (DgTea.SelectedValue).ToString() != "1" && TbColvo.Text != "0" && TbPrice.Text != "0")
            {
                teaTableAdapter.UpdateQuery(TbName.Text, TbPrice.Text, Convert.ToInt32(TbColvo.Text), (int)DgTea.SelectedValue);
                view_TeaTableAdapter.Fill(dataSet.View_Tea);
                connection.Close();
                MessageBox.Show("Чай изменён");
            }
        }

        private void BnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text != "" && TbPrice.Text != "" && TbColvo.Text != "" && (DgTea.SelectedValue).ToString() != "1")
            {
                teaTableAdapter.DeleteAll((int)DgTea.SelectedValue);
                view_TeaTableAdapter.Fill(dataSet.View_Tea);
                MessageBox.Show("Чай удален");
            }
        }

        private void DgCoffee_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DgTea.SelectedItem;
            if (dataRowView != null)
            {
                TbName.Text = dataRowView.Row.Field<String>("Название");
                TbPrice.Text = dataRowView.Row.Field<String>("Цена");
                TbColvo.Text = (dataRowView.Row.Field<Int32>("Количество")).ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgTea.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}

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
    /// Логика взаимодействия для WindowAdminAddCoffee.xaml
    /// </summary>
    public partial class WindowAdminAddCoffee : Window
    {
        Dannie dataSet;
        View_CoffeeTableAdapter view_CoffeeTableAdapter;
        CoffeeTableAdapter coffeeTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowAdminAddCoffee()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_CoffeeTableAdapter = new View_CoffeeTableAdapter();
            coffeeTableAdapter = new CoffeeTableAdapter();

            view_CoffeeTableAdapter.Fill(dataSet.View_Coffee);
            coffeeTableAdapter.Fill(dataSet.Coffee);

            DgCoffee.ItemsSource = dataSet.View_Coffee.DefaultView;
            DgCoffee.SelectionMode = DataGridSelectionMode.Single;
            DgCoffee.SelectedValuePath = "IdCoffee";
            DgCoffee.CanUserAddRows = false;
            DgCoffee.CanUserDeleteRows = false;
            DgCoffee.IsReadOnly = true;
        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text !="" && TbPrice.Text !="" && TbColvo.Text != "" && TbColvo.Text != "0" && TbPrice.Text != "0")
            {
                connection.Close();
                cmd.CommandText = "Select * from Coffee "
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
                    coffeeTableAdapter.Insert(TbName.Text, TbPrice.Text, Convert.ToInt32(TbColvo.Text));
                    view_CoffeeTableAdapter.Fill(dataSet.View_Coffee);
                    connection.Close();
                    MessageBox.Show("Кофе создано");
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Такое кофе уже есть");
                }
            }
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text != "" && TbPrice.Text != "" && TbColvo.Text != "" && (DgCoffee.SelectedValue).ToString() != "1" && TbColvo.Text != "0" && TbPrice.Text != "0")
            {
                coffeeTableAdapter.UpdateQuery(TbName.Text, TbPrice.Text, Convert.ToInt32(TbColvo.Text), (int)DgCoffee.SelectedValue);
                view_CoffeeTableAdapter.Fill(dataSet.View_Coffee);
                connection.Close();
                MessageBox.Show("Кофе изменён");
            }
        }

        private void BnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text != "" && TbPrice.Text != "" && TbColvo.Text != "" && (DgCoffee.SelectedValue).ToString() != "1")
            {
                coffeeTableAdapter.DeleteAll((int)DgCoffee.SelectedValue);
                view_CoffeeTableAdapter.Fill(dataSet.View_Coffee);
                MessageBox.Show("Кофе удален");
            }
        }

        private void DgCoffee_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DgCoffee.SelectedItem;
            if (dataRowView != null)
            {
                TbName.Text = dataRowView.Row.Field<String>("Название");
                TbPrice.Text = dataRowView.Row.Field<String>("Цена");
                TbColvo.Text = (dataRowView.Row.Field<Int32>("Количество")).ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgCoffee.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}

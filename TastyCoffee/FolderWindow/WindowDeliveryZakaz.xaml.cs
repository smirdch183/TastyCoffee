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
using TastyCoffee.FolderData;
using TastyCoffee.FolderData.DannieTableAdapters;

namespace TastyCoffee.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowDeliveryZakaz.xaml
    /// </summary>
    public partial class WindowDeliveryZakaz : Window
    {
        Dannie dataSet;
        View_MyOrbers_StatusTableAdapter view_MyOrbers_StatusTableAdapter;
        MyOrdersTableAdapter myOrdersTableAdapter;
        public string ID;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowDeliveryZakaz(string id)
        {
            InitializeComponent();
            ID = id;
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_MyOrbers_StatusTableAdapter = new View_MyOrbers_StatusTableAdapter();
            myOrdersTableAdapter = new MyOrdersTableAdapter();
            myOrdersTableAdapter.Fill(dataSet.MyOrders);
            view_MyOrbers_StatusTableAdapter.FillSotr(dataSet.View_MyOrbers_Status, 1005);

            DgZakaz.ItemsSource = dataSet.View_MyOrbers_Status.DefaultView;
            DgZakaz.SelectionMode = DataGridSelectionMode.Single;
            DgZakaz.SelectedValuePath = "IdMyOrders";
            DgZakaz.CanUserAddRows = false;
            DgZakaz.CanUserDeleteRows = false;
            DgZakaz.IsReadOnly = true;
        }

        private void DgZakaz_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new WindowDeliveryZakazInfo((DgZakaz.SelectedValue).ToString()).ShowDialog();
        }

        private void BnDostavit_Click(object sender, RoutedEventArgs e)
        {
            myOrdersTableAdapter.UpdateQuery(Convert.ToInt32(ID), (int)DgZakaz.SelectedValue);
            view_MyOrbers_StatusTableAdapter.FillSotr(dataSet.View_MyOrbers_Status, 1005);
            MessageBox.Show("Заказ добавлен");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DgZakaz.Columns[0].Visibility = Visibility.Hidden;
            DgZakaz.Columns[1].Visibility = Visibility.Hidden;
            DgZakaz.Columns[2].Visibility = Visibility.Hidden;
            DgZakaz.Columns[3].Visibility = Visibility.Hidden;
        }
    }
}

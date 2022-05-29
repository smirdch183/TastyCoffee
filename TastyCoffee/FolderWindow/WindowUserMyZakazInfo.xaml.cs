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
    /// Логика взаимодействия для WindowUserMyZakazInfo.xaml
    /// </summary>
    public partial class WindowUserMyZakazInfo : Window //Показывает всю информацию
    {
        Dannie dataSet;
        View_OrberInformationTableAdapter view_OrberInformationTableAdapter;
        OrderInformationTableAdapter orderInformationTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowUserMyZakazInfo(string MyOrder)
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_OrberInformationTableAdapter = new View_OrberInformationTableAdapter();
            orderInformationTableAdapter = new OrderInformationTableAdapter();

            view_OrberInformationTableAdapter.Fill(dataSet.View_OrberInformation, Convert.ToInt32(MyOrder));
            orderInformationTableAdapter.Fill(dataSet.OrderInformation);

            DgZakaz.ItemsSource = dataSet.View_OrberInformation.DefaultView;
            DgZakaz.SelectionMode = DataGridSelectionMode.Single;
            DgZakaz.SelectedValuePath = "IdOrderInformation";
            DgZakaz.CanUserAddRows = false;
            DgZakaz.CanUserDeleteRows = false;
            DgZakaz.IsReadOnly = true;
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

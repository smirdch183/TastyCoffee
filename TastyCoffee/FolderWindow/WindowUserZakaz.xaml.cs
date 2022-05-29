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
    /// Логика взаимодействия для WindowUserZakaz.xaml
    /// </summary>
    public partial class WindowUserZakaz : Window
    {
        Dannie dataSet;
        View_Coffee_ComboBoxTableAdapter view_Coffee_ComboBoxTableAdapter;
        View_Tea_ComboBoxTableAdapter view_Tea_ComboBoxTableAdapter;
        View_ApplicationClientTableAdapter view_ApplicationClientTableAdapter;
        ApplicationClientTableAdapter applicationClientTableAdapter;
        MyOrdersTableAdapter myOrdersTableAdapter;
        MyOrdersAndOrderInformationTableAdapter myOrdersAndOrderInformationTableAdapter;
        CoffeeTableAdapter coffeeTableAdapter;
        TeaTableAdapter teaTableAdapter;

        public string ID;
        public string ID_MyOrder;
        public string ID_ApplicationInfo;
        public string ID_OrderInfo;

        public string TeaPrice;
        public string CoffePrice;

        public string TeaColich;
        public string CoffeColich;

        public string TeaID;
        public string CoffeeID;

        public int summa;

        public int TeaIDSelect;
        public int CoffeeIDSelect;
        public string TeaColichSelect;
        public string CoffeeColichSelect;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public WindowUserZakaz(string id)
        {
            InitializeComponent();
            ID = id;
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.TastyCoffeeConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            view_Coffee_ComboBoxTableAdapter = new View_Coffee_ComboBoxTableAdapter();
            view_Tea_ComboBoxTableAdapter = new View_Tea_ComboBoxTableAdapter();
            view_ApplicationClientTableAdapter = new View_ApplicationClientTableAdapter();
            applicationClientTableAdapter = new ApplicationClientTableAdapter();
            myOrdersTableAdapter = new MyOrdersTableAdapter();
            myOrdersAndOrderInformationTableAdapter = new MyOrdersAndOrderInformationTableAdapter();
            coffeeTableAdapter = new CoffeeTableAdapter();
            teaTableAdapter = new TeaTableAdapter();


            view_Tea_ComboBoxTableAdapter.Fill(dataSet.View_Tea_ComboBox);
            view_Coffee_ComboBoxTableAdapter.Fill(dataSet.View_Coffee_ComboBox);
            view_ApplicationClientTableAdapter.Fill(dataSet.View_ApplicationClient, Convert.ToInt32(ID));
            applicationClientTableAdapter.Fill(dataSet.ApplicationClient);
            myOrdersTableAdapter.Fill(dataSet.MyOrders);
            myOrdersAndOrderInformationTableAdapter.Fill(dataSet.MyOrdersAndOrderInformation);
            coffeeTableAdapter.Fill(dataSet.Coffee);
            teaTableAdapter.Fill(dataSet.Tea);

            CbCoffe.ItemsSource = dataSet.View_Coffee_ComboBox.DefaultView;
            CbCoffe.DisplayMemberPath = "Кофе";
            CbCoffe.SelectedValuePath = "IdCoffee";
            CbCoffe.SelectedIndex = 0;

            CbTea.ItemsSource = dataSet.View_Tea_ComboBox.DefaultView;
            CbTea.DisplayMemberPath = "Чай";
            CbTea.SelectedValuePath = "IdTea";
            CbTea.SelectedIndex = 0;

            DgZakaz.ItemsSource = dataSet.View_ApplicationClient.DefaultView;
            DgZakaz.SelectionMode = DataGridSelectionMode.Single;
            DgZakaz.SelectedValuePath = "IdApplicationClient";
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

        private void DgZakaz_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DgZakaz.SelectedItem;

            if (dataRowView != null)
            {
                TbColichestvoTea.Text = dataRowView.Row.Field<String>("Кол-во чая");
                TbColichestvoCoffe.Text = dataRowView.Row.Field<String>("Кол-во кофе");
                CbCoffe.SelectedValue = dataRowView.Row.Field<int>("IdCoffee");
                CbTea.SelectedValue = dataRowView.Row.Field<int>("IdTea");
                TeaIDSelect = dataRowView.Row.Field<int>("IdTea");
                CoffeeIDSelect = dataRowView.Row.Field<int>("IdCoffee");
                TeaColichSelect = dataRowView.Row.Field<String>("Кол-во чая");
                CoffeeColichSelect = dataRowView.Row.Field<String>("Кол-во кофе");
            }
        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            summa = 0;
            if (TbColichestvoCoffe.Text != "0" || TbColichestvoTea.Text != "0")
            {
                if (TbAdress.Text != "")
                {
                    if (CbCoffe.SelectedIndex != 0 || CbTea.SelectedIndex != 0)
                    {
                        connection.Close();
                        cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + CbCoffe.SelectedValue + "' ";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CoffeeID = reader["IdCoffee"].ToString();
                            CoffeColich = reader["Quantity"].ToString();
                        }
                        connection.Close();

                        connection.Close();
                        cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + CbTea.SelectedValue + "' ";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TeaID = reader["IdTea"].ToString();
                            TeaColich = reader["Quantity"].ToString();
                        }
                        connection.Close();

                        if (Convert.ToInt32(TbColichestvoCoffe.Text) > Convert.ToInt32(CoffeColich))
                        {
                            MessageBox.Show("Такого кол-ва кофе нет на складе. Кол-во кофе на складе: " + CoffeColich);
                        }
                        else
                        {
                            if (Convert.ToInt32(TbColichestvoTea.Text) > Convert.ToInt32(TeaColich))
                            {
                                MessageBox.Show("Такого кол-ва чая нет на складе. Кол-во чая на складе: " + TeaColich);
                            }
                            else
                            {
                                applicationClientTableAdapter.Insert((int)CbCoffe.SelectedValue, (int)CbTea.SelectedValue, Convert.ToInt32(ID), TbColichestvoCoffe.Text, TbColichestvoTea.Text);
                                view_ApplicationClientTableAdapter.Fill(dataSet.View_ApplicationClient, Convert.ToInt32(ID));
                                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) - Convert.ToInt32(TbColichestvoTea.Text), Convert.ToInt32(TeaID));
                                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) - Convert.ToInt32(TbColichestvoCoffe.Text), Convert.ToInt32(CoffeeID));
                                coffeeTableAdapter.Fill(dataSet.Coffee);
                                teaTableAdapter.Fill(dataSet.Tea);
                            }
                        }
                    }
                }
            }
            var listTeaID = new List<string>();
            var listTeaColich = new List<string>();
            var listCoffeeID = new List<string>();
            var listCoffeColich = new List<string>();
            var listCoffeePrice = new List<string>();
            var listTeaPrice = new List<string>();
            connection.Close();
            cmd.CommandText = "SELECT * FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TeaID = reader["IdTea"].ToString();
                TeaColich = reader["QuantityTea"].ToString();
                CoffeeID = reader["IdCoffee"].ToString();
                CoffeColich = reader["QuantityCoffee"].ToString();
                listCoffeColich.Add(CoffeColich);
                listCoffeeID.Add(CoffeeID);
                listTeaColich.Add(TeaColich);
                listTeaID.Add(TeaID);
            }
            connection.Close();

            for (int i = 0; i < listTeaID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + listTeaID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeaPrice = reader["Price"].ToString();
                    listTeaPrice.Add(TeaPrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + listCoffeeID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CoffePrice = reader["Price"].ToString();
                    listCoffeePrice.Add(CoffePrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listTeaID.Count; i++)
            {
                summa += Convert.ToInt32(listTeaPrice[i]) * Convert.ToInt32(listTeaColich[i]);
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                summa += Convert.ToInt32(listCoffeePrice[i]) * Convert.ToInt32(listCoffeColich[i]);
            }
            LbSumm.Content = summa + " руб";
        }

        private void BnEdit_Click(object sender, RoutedEventArgs e)
        {
            summa = 0;
            if (CbCoffe.Text != "" && TbColichestvoCoffe.Text != "" || CbTea.Text != "" && TbColichestvoTea.Text != "")
            {
                if (CbCoffe.SelectedIndex != 0 || CbTea.SelectedIndex != 0)
                {
                    connection.Close();
                    cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + CbCoffe.SelectedValue + "' ";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CoffeColich = reader["Quantity"].ToString();
                    }
                    connection.Close();

                    connection.Close();
                    cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + CbTea.SelectedValue + "' ";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TeaColich = reader["Quantity"].ToString();
                    }
                    connection.Close();

                    if (Convert.ToInt32(TbColichestvoCoffe.Text) > Convert.ToInt32(CoffeColich))
                    {
                        MessageBox.Show("Такого кол-ва кофе нет на складе. Кол-во кофе на складе: " + CoffeColich);
                    }
                    else
                    {
                        if (Convert.ToInt32(TbColichestvoTea.Text) > Convert.ToInt32(TeaColich))
                        {
                            MessageBox.Show("Такого кол-ва чая нет на складе. Кол-во чая на складе: " + TeaColich);
                        }
                        else
                        {
                            connection.Close();
                            cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + TeaIDSelect + "' ";
                            cmd.Connection = connection;
                            connection.Open();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                TeaColich = reader["Quantity"].ToString();
                            }
                            connection.Close();

                            connection.Close();
                            cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + CoffeeColichSelect + "' ";
                            cmd.Connection = connection;
                            connection.Open();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                CoffeColich = reader["Quantity"].ToString();
                            }
                            connection.Close();

                            if (TbColichestvoTea.Text == TeaColichSelect && Convert.ToInt32(CbTea.SelectedValue) == TeaIDSelect)
                            {

                            }
                            else if (Convert.ToInt32(TbColichestvoTea.Text) > Convert.ToInt32(TeaColichSelect) && Convert.ToInt32(TbColichestvoTea.Text) <= Convert.ToInt32(TeaColich) && Convert.ToInt32(CbTea.SelectedValue) == TeaIDSelect)
                            {
                                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) - (Convert.ToInt32(TbColichestvoTea.Text)-Convert.ToInt32(TeaColichSelect)), Convert.ToInt32(TeaIDSelect));
                            }
                            else if (Convert.ToInt32(TbColichestvoTea.Text) < Convert.ToInt32(TeaColichSelect) && Convert.ToInt32(CbTea.SelectedValue) == TeaIDSelect)
                            {
                                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) + (Convert.ToInt32(TeaColichSelect) - Convert.ToInt32(TbColichestvoTea.Text)), Convert.ToInt32(TeaIDSelect));
                            }
                            else if (/*TbColichestvoTea.Text == TeaColichSelect &&*/ Convert.ToInt32(CbTea.SelectedValue) != TeaIDSelect)
                            {
                                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) + Convert.ToInt32(TeaColichSelect), Convert.ToInt32(TeaIDSelect));
                                connection.Close();
                                cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + CbTea.SelectedValue + "' ";
                                cmd.Connection = connection;
                                connection.Open();
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    TeaColich = reader["Quantity"].ToString();
                                }
                                connection.Close();
                                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) - Convert.ToInt32(TbColichestvoTea.Text), Convert.ToInt32(CbTea.SelectedValue));
                            }

                            if (TbColichestvoCoffe.Text == CoffeeColichSelect && Convert.ToInt32(CbCoffe.SelectedValue) == CoffeeIDSelect)
                            {

                            }
                            else if (Convert.ToInt32(TbColichestvoCoffe.Text) > Convert.ToInt32(CoffeeColichSelect) && Convert.ToInt32(TbColichestvoCoffe.Text) <= Convert.ToInt32(CoffeColich) && Convert.ToInt32(CbCoffe.SelectedValue) == CoffeeIDSelect)
                            {
                                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) - (Convert.ToInt32(TbColichestvoCoffe.Text) - Convert.ToInt32(CoffeeColichSelect)), Convert.ToInt32(CoffeeIDSelect));
                            }
                            else if (Convert.ToInt32(TbColichestvoCoffe.Text) < Convert.ToInt32(CoffeeColichSelect) && Convert.ToInt32(CbCoffe.SelectedValue) == CoffeeIDSelect)
                            {
                                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) + (Convert.ToInt32(CoffeeColichSelect) - Convert.ToInt32(TbColichestvoCoffe.Text)), Convert.ToInt32(CoffeeIDSelect));
                            }
                            else if (/*TbColichestvoCoffe.Text == CoffeeColichSelect && */Convert.ToInt32(CbCoffe.SelectedValue) != CoffeeIDSelect)
                            {
                                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) + Convert.ToInt32(CoffeeColichSelect), Convert.ToInt32(CoffeeIDSelect));
                                connection.Close();
                                cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + CbCoffe.SelectedValue + "' ";
                                cmd.Connection = connection;
                                connection.Open();
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    CoffeColich = reader["Quantity"].ToString();
                                }
                                connection.Close();
                                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) - Convert.ToInt32(TbColichestvoCoffe.Text), Convert.ToInt32(CbCoffe.SelectedValue));
                            }
                            applicationClientTableAdapter.UpdateQuery((int)CbCoffe.SelectedValue, (int)CbTea.SelectedValue, TbColichestvoCoffe.Text, TbColichestvoTea.Text, (int)DgZakaz.SelectedValue);
                            view_ApplicationClientTableAdapter.Fill(dataSet.View_ApplicationClient, Convert.ToInt32(ID));
                            coffeeTableAdapter.Fill(dataSet.Coffee);
                            teaTableAdapter.Fill(dataSet.Tea);
                        }
                    }
                }
            }
            var listTeaID = new List<string>();
            var listTeaColich = new List<string>();
            var listCoffeeID = new List<string>();
            var listCoffeColich = new List<string>();
            var listCoffeePrice = new List<string>();
            var listTeaPrice = new List<string>();
            connection.Close();
            cmd.CommandText = "SELECT * FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TeaID = reader["IdTea"].ToString();
                TeaColich = reader["QuantityTea"].ToString();
                CoffeeID = reader["IdCoffee"].ToString();
                CoffeColich = reader["QuantityCoffee"].ToString();
                listCoffeColich.Add(CoffeColich);
                listCoffeeID.Add(CoffeeID);
                listTeaColich.Add(TeaColich);
                listTeaID.Add(TeaID);
            }
            connection.Close();

            for (int i = 0; i < listTeaID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + listTeaID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeaPrice = reader["Price"].ToString();
                    listTeaPrice.Add(TeaPrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + listCoffeeID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CoffePrice = reader["Price"].ToString();
                    listCoffeePrice.Add(CoffePrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listTeaID.Count; i++)
            {
                summa += Convert.ToInt32(listTeaPrice[i]) * Convert.ToInt32(listTeaColich[i]);
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                summa += Convert.ToInt32(listCoffeePrice[i]) * Convert.ToInt32(listCoffeColich[i]);
            }
            LbSumm.Content = summa + " руб";
        }

        private void BnDelet_Click(object sender, RoutedEventArgs e)
        {
            summa = 0;
            if (CbCoffe.Text != "" && TbColichestvoCoffe.Text != "" || CbTea.Text != "" && TbColichestvoTea.Text != "")
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + TeaIDSelect + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CoffeColich = reader["Quantity"].ToString();
                }
                connection.Close();

                connection.Close();
                cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + TeaIDSelect + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeaColich = reader["Quantity"].ToString();
                }
                connection.Close();

                applicationClientTableAdapter.DeleteAll((int)DgZakaz.SelectedValue);
                view_ApplicationClientTableAdapter.Fill(dataSet.View_ApplicationClient, Convert.ToInt32(ID));
                teaTableAdapter.UpdateColich(Convert.ToInt32(TeaColich) + Convert.ToInt32(TeaColichSelect), Convert.ToInt32(TeaIDSelect));
                coffeeTableAdapter.UpdateColich(Convert.ToInt32(CoffeColich) + Convert.ToInt32(CoffeeColichSelect), Convert.ToInt32(CoffeeIDSelect));
                coffeeTableAdapter.Fill(dataSet.Coffee);
                teaTableAdapter.Fill(dataSet.Tea);
            }
            var listTeaID = new List<string>();
            var listTeaColich = new List<string>();
            var listCoffeeID = new List<string>();
            var listCoffeColich = new List<string>();
            var listCoffeePrice = new List<string>();
            var listTeaPrice = new List<string>();
            connection.Close();
            cmd.CommandText = "SELECT * FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TeaID = reader["IdTea"].ToString();
                TeaColich = reader["QuantityTea"].ToString();
                CoffeeID = reader["IdCoffee"].ToString();
                CoffeColich = reader["QuantityCoffee"].ToString();
                listCoffeColich.Add(CoffeColich);
                listCoffeeID.Add(CoffeeID);
                listTeaColich.Add(TeaColich);
                listTeaID.Add(TeaID);
            }
            connection.Close();

            for (int i = 0; i < listTeaID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Tea where IdTea = '" + listTeaID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeaPrice = reader["Price"].ToString();
                    listTeaPrice.Add(TeaPrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                connection.Close();
                cmd.CommandText = "SELECT * FROM Coffee where IdCoffee = '" + listCoffeeID[i] + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CoffePrice = reader["Price"].ToString();
                    listCoffeePrice.Add(CoffePrice);
                }
                connection.Close();
            }
            for (int i = 0; i < listTeaID.Count; i++)
            {
                summa += Convert.ToInt32(listTeaPrice[i]) * Convert.ToInt32(listTeaColich[i]);
            }
            for (int i = 0; i < listCoffeeID.Count; i++)
            {
                summa += Convert.ToInt32(listCoffeePrice[i]) * Convert.ToInt32(listCoffeColich[i]);
            }
            LbSumm.Content = summa + " руб";
        }

        private void BnZakaz_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            cmd.CommandText = "INSERT INTO OrderInformation SELECT * FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            connection.Close();

            myOrdersTableAdapter.Insert(Convert.ToInt32(ID), 1005, 1, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), TbAdress.Text);

            connection.Close();
            cmd.CommandText = "SELECT * FROM MyOrders where Date = '" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "' " + "and IdClient = '" + ID + "'";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ID_MyOrder = reader["IdMyOrders"].ToString();
            }
            connection.Close();

            var list = new List<string>();

            connection.Close();
            cmd.CommandText = "SELECT * FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ID_ApplicationInfo = reader["IdApplicationClient"].ToString();
                list.Add(ID_ApplicationInfo);
            }
            connection.Close();

            for (int i = 0; i < list.Count; i++)
            {
                cmd.CommandText = "SELECT * FROM OrderInformation where IdApplicationClient = '" + list[i] + "' " + "and IdClient = '" + ID + "'";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ID_OrderInfo = reader["IdOrderInformation"].ToString();
                    myOrdersAndOrderInformationTableAdapter.Insert(Convert.ToInt32(ID_MyOrder), Convert.ToInt32(ID_OrderInfo));
                }
                connection.Close();
            }
            list.Clear();

            connection.Close();
            cmd.CommandText = "DELETE FROM ApplicationClient where IdClient = '" + ID + "' ";
            cmd.Connection = connection;
            connection.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            connection.Close();

            view_ApplicationClientTableAdapter.Fill(dataSet.View_ApplicationClient, Convert.ToInt32(ID));
            CbTea.SelectedIndex = 0;
            CbCoffe.SelectedIndex = 0;
            TbAdress.Text = "";
            TbColichestvoCoffe.Text = "";
            TbColichestvoTea.Text = "";
        }

        private void CbCoffe_Selected(object sender, RoutedEventArgs e)
        {
            if (CbCoffe.SelectedIndex == 0)
            {
                TbColichestvoCoffe.Text = "0";
                TbColichestvoCoffe.IsEnabled = false;
            }
            else
            {
                TbColichestvoCoffe.IsEnabled = true;
            }
        }

        private void CbTea_Selected(object sender, RoutedEventArgs e)
        {
            if (CbTea.SelectedIndex == 0)
            {
                TbColichestvoTea.Text = "0";
                TbColichestvoTea.IsEnabled = false;
            }
            else
            {
                TbColichestvoTea.IsEnabled = true;
            }
        }
    }
}

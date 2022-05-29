using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для WindowAdmnSelectionOfProducts.xaml
    /// </summary>
    public partial class WindowAdmnSelectionOfProducts : Window
    {
        public WindowAdmnSelectionOfProducts()
        {
            InitializeComponent();
        }

        private void BnCoffe_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminAddCoffee windowAdminAddCoffee = new WindowAdminAddCoffee();
            windowAdminAddCoffee.Show();
            this.Close();
        }

        private void BnTae_Click(object sender, RoutedEventArgs e)
        {
            WindowAdminAddTae windowAdminAddTae = new WindowAdminAddTae();
            windowAdminAddTae.Show();
            this.Close();
        }
    }
}

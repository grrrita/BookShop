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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Outbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Shopbtn_Click(object sender, RoutedEventArgs e)
        {
            ShopsWindow shopsWindow = new ShopsWindow();
            shopsWindow.Show();
        }

        private void Bookbtn_Click(object sender, RoutedEventArgs e)
        {
            BooksWindow booksWindow = new BooksWindow();
            booksWindow.Show();
        }

        private void Availbtn_Click(object sender, RoutedEventArgs e)
        {
            AvailWindow availWindow=new AvailWindow();
            availWindow.Show();
        }
    }
}

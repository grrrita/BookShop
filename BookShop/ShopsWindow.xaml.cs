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

namespace BookShop
{
    /// <summary>
    /// Логика взаимодействия для ShopsWindow.xaml
    /// </summary>
    public partial class ShopsWindow : Window
    {
        public ShopsWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var shops = db.Shops.ToList();
                ShopsList.DataContext = shops;
                //MessageBox.Show(users.Count.ToString());
            }
        }
        private void ShowMethod()
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var shops = db.Shops.ToList();
                ShopsList.DataContext = shops;
            }
        }
        private void AddShop(Shop Addshop)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // Добавление
                db.Shops.Add(Addshop);
                db.SaveChanges();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbAddress.Text.Length < 1)
            {
                MessageBox.Show("Введите адрес");
                return;
            }
            if (tbDirector.Text.Length < 1)
            {
                MessageBox.Show("Введите имя директора");
                return;
            }
         
            string address = tbAddress.Text;
            string director = tbDirector.Text;
            Shop Addshop = new Shop { Address = address, Director = director};
            AddShop(Addshop);
            ShowMethod();
        }
        private void UpdateShop(Shop updateShop)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Shop shop = db.Shops.Where(p => p.Id == updateShop.Id).FirstOrDefault();
                if (shop != null)
                {
                    shop.Address = updateShop.Address;
                    shop.Director = updateShop.Director;
                    //обновляем объект
                    db.Shops.Update(shop);
                    db.SaveChanges();
                }
            }
        }
        private void DeleteShop(int id)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Shop shop = db.Shops.Where(p => p.Id == id).FirstOrDefault();
                if (shop != null)
                {
                    //удаляем объект
                    db.Shops.Remove(shop);
                    db.SaveChanges();
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ShopsList.SelectedItems.Count > 0)
            {
                int index = ShopsList.SelectedIndex;
                var deleteshop = (Shop)ShopsList.Items[index];

                MessageBoxResult result = MessageBox.Show("Действительно удалить запись в БД?", "Удаление магазина", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteShop(deleteshop.Id);
                    ShowMethod();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ShopsList.SelectedItems.Count > 0)
            {

                if (tbAddress.Text.Length < 1)
                {
                    MessageBox.Show("Введите адрес");
                    return;
                }
                if (tbDirector.Text.Length < 1)
                {
                    MessageBox.Show("Введите имя директора");
                    return;
                }

                int index = ShopsList.SelectedIndex;
                var updateshop = (Shop)ShopsList.Items[index];

                string address = tbAddress.Text;
                string director = tbDirector.Text;
                updateshop.Address = address;
                updateshop.Director = director;
                UpdateShop(updateshop);
                ShowMethod();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbAddress.Text = "";
            tbDirector.Text = "";
        }

        private void ShopsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var shop = (Shop)ShopsList.SelectedItem;
            using (BookShopContext db = new BookShopContext())
            {
                var avail = db.Avails.Where(x => x.IdShop == shop.Id).ToList();
                List<int> ids = avail.Select(x => x.IdBook).ToList();
                var books = db.Books.Where(x => ids.Contains(x.Id)).ToList();
                BooksList.DataContext = books;

            }
        }
    }
    
}

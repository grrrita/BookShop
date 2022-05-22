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
    /// Логика взаимодействия для AvailWindow.xaml
    /// </summary>
    public partial class AvailWindow : Window
    {
        public AvailWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var avails = db.Avails.ToList();
                AvailsList.DataContext = avails;
                //MessageBox.Show(users.Count.ToString());
            }
        }
        private void ShowMethod()
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var avails = db.Avails.ToList();
                AvailsList.DataContext = avails;
            }
        }
        private void AddAvail(Avail Addavail)
        {
            
            using (BookShopContext db = new BookShopContext())
            {
                Avail avail = db.Avails.Where(p => p.IdShop == Addavail.IdShop && p.IdBook == Addavail.IdBook).FirstOrDefault();
                // Добавление
                if (avail == null)
                {
                    db.Avails.Add(Addavail);
                }
                else
                {
                    avail.Count+=Addavail.Count;
                    db.Avails.Update(avail);
                }
                db.SaveChanges();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbShop.Text.Length < 1)
            {
                MessageBox.Show("Введите магазин");
                return;
            }
            if (tbBook.Text.Length < 1)
            {
                MessageBox.Show("Введите книгу");
                return;
            }
            if (tbCount.Text.Length < 1)
            {
                MessageBox.Show("Введите количество");
                return;
            }
            int shop = Convert.ToInt32(tbShop.Text);
            int book = Convert.ToInt32(tbBook.Text);
            int count = Convert.ToInt32(tbCount.Text);
            Avail Addavail = new Avail { IdShop = shop,  IdBook= book, Count = count };
            AddAvail(Addavail);
            ShowMethod();
        }
        private void UpdateAvail(Avail updateAvail)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Avail avail = db.Avails.Where(p => p.IdShop == updateAvail.IdShop&&p.IdBook==updateAvail.IdBook).FirstOrDefault();
                if (avail != null)
                {
                    avail.Count = updateAvail.Count;
                    //обновляем объект
                    db.Avails.Update(avail);
                    db.SaveChanges();
                }
            }
        }
        private void DeleteBook(int idshop,int idbook)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Avail avail = db.Avails.Where(p => p.IdShop == idshop&&p.IdBook==idbook).FirstOrDefault();
                if (avail != null)
                {
                    //удаляем объект
                    db.Avails.Remove(avail);
                    db.SaveChanges();
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AvailsList.SelectedItems.Count > 0)
            {
                int index = AvailsList.SelectedIndex;
                var deleteavail = (Avail)AvailsList.Items[index];

                MessageBoxResult result = MessageBox.Show("Действительно удалить запись в БД?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteBook(deleteavail.IdShop,deleteavail.IdBook);
                    ShowMethod();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (AvailsList.SelectedItems.Count > 0)
            {

                if (tbShop.Text.Length < 1)
                {
                    MessageBox.Show("Введите магазин");
                    return;
                }
                if (tbBook.Text.Length < 1)
                {
                    MessageBox.Show("Введите книгу");
                    return;
                }
                if (tbCount.Text.Length < 1)
                {
                    MessageBox.Show("Введите количество");
                    return;
                }
                int index = AvailsList.SelectedIndex;
                var updateavail = (Avail)AvailsList.Items[index];

                int shop = Convert.ToInt32(tbShop.Text);
                int book = Convert.ToInt32(tbBook.Text);
                int count = Convert.ToInt32(tbCount.Text);
                updateavail.IdShop = shop;
                updateavail.IdBook = book;
                updateavail.Count = count;
                UpdateAvail(updateavail);
                ShowMethod();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbShop.Text = "";
            tbBook.Text = "";
            tbCount.Text = "";
        }

        
        private void AvailsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (BookShopContext db = new BookShopContext())
            {
                var av = AvailsList.SelectedItem as Avail;
                
                if (av != null)
                {
                    var b = db.Books.First(x => x.Id == av.IdBook);
                    var sh = db.Shops.First(x => x.Id == av.IdShop);
                    if (b != null && sh != null)
                    {
                        tbNameOfBook.Text = b.Name;
                        tbAithor.Text = b.Author;
                        tbDir.Text = sh.Director;
                        tbAdd.Text = sh.Address;
                    }
                }
            }
        }
    }
}

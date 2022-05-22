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
    /// Логика взаимодействия для BooksWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        public BooksWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var books = db.Books.ToList();
                BooksList.DataContext = books;
                //MessageBox.Show(users.Count.ToString());
            }
        }
        private void ShowMethod()
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем объекты из бд и выводим на консоль
                var books = db.Books.ToList();
                BooksList.DataContext = books;
            }
        }
        private void AddBook(Book Addbook)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // Добавление
                db.Books.Add(Addbook);
                db.SaveChanges();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length < 1)
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (tbAuthor.Text.Length < 1)
            {
                MessageBox.Show("Введите автора");
                return;
            }
            if (tbPrice.Text.Length < 1)
            {
                MessageBox.Show("Введите цену");
                return;
            }
            string name = tbName.Text;
            string author = tbAuthor.Text;
            int price = Convert.ToInt32(tbPrice.Text);
            Book Addbook = new Book { Name = name, Author = author, Price = price };
            AddBook(Addbook);
            ShowMethod();
        }
        private void UpdateBook(Book updateBook)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Book book = db.Books.Where(p => p.Id == updateBook.Id).FirstOrDefault();
                if (book != null)
                {
                    book.Name = updateBook.Name;
                    book.Author = updateBook.Author;
                    book.Price = updateBook.Price;
                    //обновляем объект
                    db.Books.Update(book);
                    db.SaveChanges();
                }
            }
        }
        private void DeleteBook(int id)
        {
            using (BookShopContext db = new BookShopContext())
            {
                // получаем первый объект
                Book book = db.Books.Where(p => p.Id == id).FirstOrDefault();
                if (book != null)
                {
                    //удаляем объект
                    db.Books.Remove(book);
                    db.SaveChanges();
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItems.Count > 0)
            {
                int index = BooksList.SelectedIndex;
                var deletebook = (Book)BooksList.Items[index];

                MessageBoxResult result = MessageBox.Show("Действительно удалить запись в БД?", "Удаление книги", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteBook(deletebook.Id);
                    ShowMethod();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItems.Count > 0)
            {

                if (tbName.Text.Length < 1)
                {
                    MessageBox.Show("Введите название");
                    return;
                }
                if (tbAuthor.Text.Length < 1)
                {
                    MessageBox.Show("Введите автора");
                    return;
                }
                if (tbPrice.Text.Length < 1)
                {
                    MessageBox.Show("Введите цену");
                    return;
                }
                int index = BooksList.SelectedIndex;
                var updatebook = (Book)BooksList.Items[index];

                string name = tbName.Text;
                string author = tbAuthor.Text;
                int price = Convert.ToInt32(tbPrice.Text);
                updatebook.Name = name;
                updatebook.Author = author;
                updatebook.Price = price;
                UpdateBook(updatebook);
                ShowMethod();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            tbAuthor.Text = "";
            tbPrice.Text = "";
        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var book = (Book)BooksList.SelectedItem;
            using (BookShopContext db = new BookShopContext())
            {
                var avail = db.Avails.Where(x => x.IdBook == book.Id).ToList();
                List<int> ids = avail.Select(x => x.IdShop).ToList();
                var shops = db.Shops.Where(x => ids.Contains(x.Id)).ToList();
                ShopsList.DataContext = shops;

            }
        }
    }
}

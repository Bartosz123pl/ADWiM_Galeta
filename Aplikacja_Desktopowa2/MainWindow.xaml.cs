using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;
using Aplikacja_Desktopowa2.Views;

namespace Aplikacja_Desktopowa2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadBooks();
            LoadAuthors();
            LoadPublishers();
        }

        // KSIĄŻKI

        private void LoadBooks()
        {
            using (var db = new WarehouseContext())
            {
                var books = db.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .ToList();

                BooksGrid.ItemsSource = books;
            }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            BookWindow window = new BookWindow();
            window.Owner = this;

            if (window.ShowDialog() == true)
                LoadBooks();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BooksGrid.SelectedItem as Book;

            if (selected == null)
            {
                MessageBox.Show("Wybierz książkę.");
                return;
            }

            BookWindow window = new BookWindow(selected);
            window.Owner = this;

            if (window.ShowDialog() == true)
                LoadBooks();
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BooksGrid.SelectedItem as Book;

            if (selected == null)
            {
                MessageBox.Show("Wybierz książkę.");
                return;
            }

            if (MessageBox.Show(
                "Czy usunąć książkę " + selected.Title + "?",
                "Potwierdzenie",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            using (var db = new WarehouseContext())
            {
                Book book = db.Books.Find(selected.Id);

                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                }
            }

            LoadBooks();
        }

        // WYSZUKIWANIE

        private void SearchBox_TextChanged(
            object sender,
            System.Windows.Controls.TextChangedEventArgs e)
        {
            string search = SearchBox.Text.Trim();

            using (var db = new WarehouseContext())
            {
                var query = db.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(b =>
                        b.Title.Contains(search) ||
                        b.Author.LastName.Contains(search) ||
                        b.Publisher.Name.Contains(search));
                }

                BooksGrid.ItemsSource = query.ToList();
            }
        }

        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Clear();
            LoadBooks();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
            LoadAuthors();
            LoadPublishers();
        }

        // AUTORZY

        private void LoadAuthors()
        {
            using (var db = new WarehouseContext())
            {
                AuthorsGrid.ItemsSource = db.Authors
                    .OrderBy(a => a.LastName)
                    .ToList();
            }
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AuthorWindow window = new AuthorWindow();
            window.Owner = this;

            if (window.ShowDialog() == true)
                LoadAuthors();
        }

        private void EditAuthor_Click(object sender, RoutedEventArgs e)
        {
            Author selected = AuthorsGrid.SelectedItem as Author;

            if (selected == null)
            {
                MessageBox.Show("Wybierz autora.");
                return;
            }

            AuthorWindow window = new AuthorWindow(selected);
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                LoadAuthors();
                LoadBooks();
            }
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            Author selected = AuthorsGrid.SelectedItem as Author;

            if (selected == null)
            {
                MessageBox.Show("Wybierz autora.");
                return;
            }

            using (var db = new WarehouseContext())
            {
                if (db.Books.Any(b => b.AuthorId == selected.Id))
                {
                    MessageBox.Show(
                        "Nie można usunąć autora, który posiada książki.");
                    return;
                }

                Author author = db.Authors.Find(selected.Id);

                if (author != null)
                {
                    db.Authors.Remove(author);
                    db.SaveChanges();
                }
            }

            LoadAuthors();
        }

        // WYDAWNICTWA

        private void LoadPublishers()
        {
            using (var db = new WarehouseContext())
            {
                PublishersGrid.ItemsSource = db.Publishers
                    .OrderBy(p => p.Name)
                    .ToList();
            }
        }

        private void AddPublisher_Click(object sender, RoutedEventArgs e)
        {
            PublisherWindow window = new PublisherWindow();
            window.Owner = this;

            if (window.ShowDialog() == true)
                LoadPublishers();
        }

        private void EditPublisher_Click(object sender, RoutedEventArgs e)
        {
            Publisher selected =
                PublishersGrid.SelectedItem as Publisher;

            if (selected == null)
            {
                MessageBox.Show("Wybierz wydawnictwo.");
                return;
            }

            PublisherWindow window = new PublisherWindow(selected);
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                LoadPublishers();
                LoadBooks();
            }
        }

        private void DeletePublisher_Click(object sender, RoutedEventArgs e)
        {
            Publisher selected =
                PublishersGrid.SelectedItem as Publisher;

            if (selected == null)
            {
                MessageBox.Show("Wybierz wydawnictwo.");
                return;
            }

            using (var db = new WarehouseContext())
            {
                if (db.Books.Any(b => b.PublisherId == selected.Id))
                {
                    MessageBox.Show(
                        "Nie można usunąć wydawnictwa, które posiada książki.");
                    return;
                }

                Publisher publisher =
                    db.Publishers.Find(selected.Id);

                if (publisher != null)
                {
                    db.Publishers.Remove(publisher);
                    db.SaveChanges();
                }
            }

            LoadPublishers();
        }
    }
}

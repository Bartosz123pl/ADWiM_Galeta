
using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;

namespace Aplikacja_Desktopowa2.Views
{
    public partial class BookWindow : Window
    {
        private readonly WarehouseContext db;
        private readonly Book book;

        public BookWindow(Book selectedBook = null)
        {
            InitializeComponent();

            db = new WarehouseContext();
            book = selectedBook;

            AuthorCombo.ItemsSource = db.Authors
                .OrderBy(a => a.LastName)
                .ToList();

            PublisherCombo.ItemsSource = db.Publishers
                .OrderBy(p => p.Name)
                .ToList();

            if (book != null)
            {
                Title = "Edycja książki";

                TitleBox.Text = book.Title;
                ISBNBox.Text = book.ISBN;
                PriceBox.Text = book.Price.ToString(
                    CultureInfo.CurrentCulture);
                QuantityBox.Text = book.Quantity.ToString();

                AuthorCombo.SelectedValue = book.AuthorId;
                PublisherCombo.SelectedValue = book.PublisherId;

                AuthorCombo.SelectedValuePath = "Id";
                PublisherCombo.SelectedValuePath = "Id";
            }
            else
            {
                AuthorCombo.SelectedValuePath = "Id";
                PublisherCombo.SelectedValuePath = "Id";
            }

            Closed += (s, e) => db.Dispose();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) ||
                string.IsNullOrWhiteSpace(ISBNBox.Text) ||
                AuthorCombo.SelectedItem == null ||
                PublisherCombo.SelectedItem == null)
            {
                MessageBox.Show("Uzupełnij wszystkie wymagane pola.");
                return;
            }

            if (!decimal.TryParse(
                PriceBox.Text,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal price) || price < 0)
            {
                MessageBox.Show("Nieprawidłowa cena.");
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int quantity) ||
                quantity < 0)
            {
                MessageBox.Show("Nieprawidłowa ilość.");
                return;
            }

            if (db.Books.Any(b => b.ISBN == ISBNBox.Text.Trim()
                && (book == null || b.Id != book.Id)))
            {
                MessageBox.Show("Książka o takim ISBN już istnieje.");
                return;
            }

            if (book == null)
            {
                Book newBook = new Book();
                db.Books.Add(newBook);

                ApplyChanges(newBook, price, quantity);
            }
            else
            {
                Book existingBook = db.Books.Find(book.Id);

                if (existingBook == null)
                {
                    MessageBox.Show("Nie znaleziono książki.");
                    return;
                }

                ApplyChanges(existingBook, price, quantity);
            }

            try
            {
                db.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu:\n" + ex.Message);
            }
        }

        private void ApplyChanges(Book target,
            decimal price, int quantity)
        {
            target.Title = TitleBox.Text.Trim();
            target.ISBN = ISBNBox.Text.Trim();
            target.Price = price;
            target.Quantity = quantity;

            target.AuthorId =
                (int)AuthorCombo.SelectedValue;

            target.PublisherId =
                (int)PublisherCombo.SelectedValue;
        }
    }
}

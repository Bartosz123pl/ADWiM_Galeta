
using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;
using System;
using System.Linq;
using System.Windows;
using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;

namespace Aplikacja_Desktopowa2.Views
{
    public partial class AuthorWindow : Window
    {
        private readonly WarehouseContext db;
        private readonly Author author;

        public AuthorWindow(Author selectedAuthor = null)
        {
            InitializeComponent();

            db = new WarehouseContext();
            author = selectedAuthor;

            if (author != null)
            {
                Title = "Edycja autora";
                FirstNameBox.Text = author.FirstName;
                LastNameBox.Text = author.LastName;
            }

            Closed += (s, e) => db.Dispose();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text.Trim();
            string lastName = LastNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Uzupełnij imię i nazwisko.");
                return;
            }

            if (db.Authors.Any(a =>
                a.FirstName == firstName &&
                a.LastName == lastName &&
                (author == null || a.Id != author.Id)))
            {
                MessageBox.Show("Taki autor już istnieje.");
                return;
            }

            try
            {
                Author target;

                if (author == null)
                {
                    target = new Author();
                    db.Authors.Add(target);
                }
                else
                {
                    target = db.Authors.Find(author.Id);

                    if (target == null)
                    {
                        MessageBox.Show("Nie znaleziono autora.");
                        return;
                    }
                }

                target.FirstName = firstName;
                target.LastName = lastName;

                db.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu:\n" + ex.Message);
            }
        }
    }
}


using Aplikacja_Desktopowa2.Data;
using System;
using System.Linq;
using System.Windows;
using Aplikacja_Desktopowa2.Data;
using Aplikacja_Desktopowa2.Models;

namespace Aplikacja_Desktopowa2.Views
{
    public partial class PublisherWindow : Window
    {
        private readonly WarehouseContext db;
        private readonly Publisher publisher;

        public PublisherWindow(Publisher selectedPublisher = null)
        {
            InitializeComponent();

            db = new WarehouseContext();
            publisher = selectedPublisher;

            if (publisher != null)
            {
                Title = "Edycja wydawnictwa";
                NameBox.Text = publisher.Name;
                AddressBox.Text = publisher.Address;
            }

            Closed += (s, e) => db.Dispose();
        }
private void Save_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string address = AddressBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Podaj nazwę wydawnictwa.");
                return;
            }

            bool exists;

            if (publisher == null)
            {
                exists = db.Publishers.Any(p => p.Name == name);
            }
            else
            {
                int currentId = publisher.Id;

                exists = db.Publishers.Any(p =>
                    p.Name == name && p.Id != currentId);
            }

            if (exists)
            {
                MessageBox.Show("Takie wydawnictwo już istnieje.");
                return;
            }

            try
            {
                Publisher target;

                if (publisher == null)
                {
                    target = new Publisher();
                    db.Publishers.Add(target);
                }
                else
                {
                    target = db.Publishers.Find(publisher.Id);

                    if (target == null)
                    {
                        MessageBox.Show("Nie znaleziono wydawnictwa.");
                        return;
                    }
                }

                target.Name = name;
                target.Address = address;

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

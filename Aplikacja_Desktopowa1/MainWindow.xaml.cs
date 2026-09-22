using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Aplikacja_Desktopowa1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
    
        public List<Pracownik> listaPracownikow = new List<Pracownik>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonDodaj(object sender, RoutedEventArgs e)
        {
            Dodaj okno = new Dodaj(listaPracownikow);
            okno.ShowDialog();

            // tutaj możesz odświeżyć ListBox
            Listbox1.ItemsSource = null;
            Listbox1.ItemsSource = listaPracownikow;
        }
        private void ButtonUsun(object sender, RoutedEventArgs e)
        {
            if (Listbox1.SelectedItem is Pracownik pracownik)
            {
                listaPracownikow.Remove(pracownik);

                Listbox1.ItemsSource = null;
                Listbox1.ItemsSource = listaPracownikow;
            }
        }
        private void ButtonZobacz(object sender, RoutedEventArgs e)
        {
            if (Listbox1.SelectedItem is Pracownik pracownik)
            {
                Zobacz okno = new Zobacz(pracownik);
                okno.ShowDialog();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
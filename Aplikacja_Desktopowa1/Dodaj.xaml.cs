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

namespace Aplikacja_Desktopowa1
{
    /// <summary>
    /// Logika interakcji dla klasy Dodaj.xaml
    /// </summary>
    public partial class Dodaj : Window
    {
        private List<Pracownik> listaPracownikow;

        public Dodaj(List<Pracownik> listaPracownikow)
        {
            InitializeComponent();

            this.listaPracownikow = listaPracownikow;
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            char dziesiataCyfraChar = input4.Text[9];
            int cyfraPlci = int.Parse(dziesiataCyfraChar.ToString());

            Pracownik nowyPracownik = new Pracownik();

            nowyPracownik.Imie = input1.Text;
            nowyPracownik.Nazwisko = input2.Text;
            nowyPracownik.Wyplata = input3.Text;
            nowyPracownik.M = (cyfraPlci % 2 != 0);
            nowyPracownik.PESEL = input4.Text;
            nowyPracownik.Menadzer = (bool)menadzer.IsChecked;

            listaPracownikow.Add(nowyPracownik);

            Close();
        }
    }
}
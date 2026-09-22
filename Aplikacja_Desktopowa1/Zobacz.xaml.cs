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
    /// Logika interakcji dla klasy Zobacz.xaml
    /// </summary>
    public partial class Zobacz : Window
    {
        private Pracownik pracownik;

        public Zobacz(Pracownik pracownik)
        {
            InitializeComponent();

            this.pracownik = pracownik;

            Imie.Content = pracownik.Imie;
            Nazwisko.Content = pracownik.Nazwisko;
            PESEL.Content = pracownik.PESEL;

            if (pracownik.M)
            {
                Plec.Content = "Mężczyzna";
            }
            else
            {
                Plec.Content = "Kobieta";
            }

            if (pracownik.Menadzer)
            {
                Menadzer.Content = "Tak";
            }
            else
            {
                Menadzer.Content = "Nie";
            }
        }
    }
}
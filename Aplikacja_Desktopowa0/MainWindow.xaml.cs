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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplikacja_Desktopowa0
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Oblicz(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(input1.Text, out double a) ||
                !double.TryParse(input2.Text, out double b) ||
                !double.TryParse(input3.Text, out double c))
            {
                Wynik.Content = "Podaj poprawne wartości a, b i c.";
                return;
            }

            if (a == 0)
            {
                Wynik.Content = "Wartość a nie może być równa 0.";
                return;
            }

            double delta = b * b - 4 * a * c;

            if (delta > 0)
            {
                double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b + Math.Sqrt(delta)) / (2 * a);

                Wynik.Content = $"Δ = {delta}\n" +
                             $"x₁ = {x1}\n" +
                             $"x₂ = {x2}";
            }
            else if (delta == 0)
            {
                double x0 = -b / (2 * a);

                Wynik.Content = $"Δ = {delta}\n" +
                             $"x₀ = {x0}";
            }
            else
            {
                Wynik.Content = $"Δ = {delta}\n" +
                             "Brak pierwiastków rzeczywistych.";
            }
        }
    }
}

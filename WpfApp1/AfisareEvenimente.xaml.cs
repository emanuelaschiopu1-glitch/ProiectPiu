using System.Windows;
using LibrarieModele;
using NivelStocareDate;

namespace WpfApp1
{
    public partial class AfisareEvenimente : Window
    {
        IStocareData stocare = new AdministrareEvenimenteFisierText("Evenimente.txt");

        public AfisareEvenimente()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            // Luăm lista de obiecte din fișier
            var lista = stocare.GetEvenimente();

            lstEvenimente.Items.Clear();
            foreach (var ev in lista)
            {
                // Adăugăm în ListBox string-ul formatat în clasa Eveniment
                lstEvenimente.Items.Add(ev.Info());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
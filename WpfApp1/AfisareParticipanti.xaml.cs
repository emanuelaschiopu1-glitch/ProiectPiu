using System.Windows;
using LibrarieModele;
using NivelStocareDate;

namespace WpfApp1
{
    public partial class AfisareParticipanti : Window
    {
        // Folosim aceeași stocare ca la evenimente
        IStocareData stocare = new AdministrareEvenimenteFisierText("Evenimente.txt");

        public AfisareParticipanti()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            // Apelăm metoda din stocare
            var lista = stocare.GetParticipanti();

            lstParticipanti.Items.Clear();
            if (lista != null && lista.Count > 0)
            {
                foreach (var p in lista)
                {
                    lstParticipanti.Items.Add(p.Info());
                }
            }
            else
            {
                lstParticipanti.Items.Add("Nu există participanți salvați.");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
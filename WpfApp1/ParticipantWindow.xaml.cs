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

namespace WpfApp1
{
    public partial class ParticipantWindow : Window
    {
        // Refolosim MainViewModel pentru a avea acces la ParticipantNou
        MainViewModel viewModel = new MainViewModel();

        public ParticipantWindow()
        {
            InitializeComponent();

            // Această linie leagă XAML-ul de datele noastre
            this.DataContext = viewModel;
        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            // Verificăm dacă obiectul are erori (din IDataErrorInfo)
            if (string.IsNullOrEmpty(viewModel.ParticipantNou["Nume"]) &&
                string.IsNullOrEmpty(viewModel.ParticipantNou["Email"]))
            {
                // Aici apelăm salvarea (ex: în fișier sau memorie)
                MessageBox.Show($"Participantul {viewModel.ParticipantNou.Nume} a fost adăugat!");
                this.Close(); // Închidem fereastra după succes
            }
            else
            {
                MessageBox.Show("Te rugăm să corectezi erorile marcate cu roșu!");
            }
        }
    }
}

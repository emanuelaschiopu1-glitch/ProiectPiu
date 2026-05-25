using System.Windows;
using NivelStocareDate;
namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenEvenimente_Click(object sender, RoutedEventArgs e)
        {
            EvenimentWindow evWin = new EvenimentWindow();
            evWin.ShowDialog();
        }

        private void OpenParticipanti_Click(object sender, RoutedEventArgs e)
        {
            ParticipantWindow partWin = new ParticipantWindow();
            partWin.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenListaParticipanti_Click(object sender, RoutedEventArgs e)
        {
            // Verifică dacă ai creat fereastra AfisareParticipanti la pasul anterior
            AfisareParticipanti listaPartWin = new AfisareParticipanti();
            listaPartWin.ShowDialog();
        }
        private void OpenLista_Click(object sender, RoutedEventArgs e)
        {
            // Totul pe o singură linie sau unit corect
            AfisareEvenimente afisWin = new AfisareEvenimente();
            afisWin.ShowDialog();
        }
    }
}
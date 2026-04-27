using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Cerința 2: Constante
        private const int MAX_LIMIT = 15;
        private readonly SolidColorBrush culoareVerde = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F5D50"));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (Valideaza() == 0)
            {
                lstEvenimente.Items.Add($"{txtNume.Text} (@{txtLocatie.Text})");
                MessageBox.Show("Eveniment adăugat!");
                btnReset_Click(sender, e);
            }
        }

        private int Valideaza()
        {
            int erori = 0;
            lblNume.Foreground = culoareVerde;
            lblLocatie.Foreground = culoareVerde;
            errNume.Visibility = Visibility.Collapsed;
            errLocatie.Visibility = Visibility.Collapsed;

            // Validare Nume
            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > MAX_LIMIT)
            {
                lblNume.Foreground = Brushes.Red;
                errNume.Visibility = Visibility.Visible;
                erori++;
            }

            // Validare Locație
            if (string.IsNullOrWhiteSpace(txtLocatie.Text))
            {
                lblLocatie.Foreground = Brushes.Red;
                errLocatie.Visibility = Visibility.Visible;
                erori++;
            }

            return erori;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtNume.Clear();
            txtLocatie.Clear();
            lblNume.Foreground = culoareVerde;
            lblLocatie.Foreground = culoareVerde;
            errNume.Visibility = Visibility.Collapsed;
            errLocatie.Visibility = Visibility.Collapsed;
        }
    }
}
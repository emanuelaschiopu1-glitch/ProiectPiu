using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Cerința 2: Listă pentru stocarea entităților
        private List<string> listaDate = new List<string>();

        // Cerința 2: Constantă pentru validare
        private const int MAX_LUNGIME_NUME = 15;
        private readonly SolidColorBrush culoareVerde = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F5D50"));

        public MainWindow()
        {
            InitializeComponent();
        }

        // Cerința 2 & 3: Adăugarea entității cu validare
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (ValidareDate() == 0)
            {
                string tip = rbPublic.IsChecked == true ? "Public" : "Privat";
                string optiune = chkVIP.IsChecked == true ? "[VIP]" : "[Standard]";

                string entitateNoua = $"{txtNume.Text.ToUpper()} - {txtLocatie.Text} ({tip}) {optiune}";

                listaDate.Add(entitateNoua);
                ActualizeazaInterfata(listaDate);

                MessageBox.Show("Entitate adăugată cu succes!");
                btnReset_Click(sender, e);
            }
        }

        // Cerința 3: Operația de căutare (filtrare în timp real)
        private void txtCautare_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string textCautat = txtCautare.Text.ToLower();
            var rezultate = listaDate.Where(x => x.ToLower().Contains(textCautat)).ToList();
            ActualizeazaInterfata(rezultate);
        }

        private int ValidareDate()
        {
            int erori = 0;
            ResetareCulori();

            // Validare nume folosind constanta
            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > MAX_LUNGIME_NUME)
            {
                lblNume.Foreground = Brushes.Red;
                erori++;
            }

            if (string.IsNullOrWhiteSpace(txtLocatie.Text))
            {
                lblLocatie.Foreground = Brushes.Red;
                erori++;
            }

            return erori;
        }

        private void ActualizeazaInterfata(List<string> sursa)
        {
            lstEvenimente.ItemsSource = null;
            lstEvenimente.ItemsSource = sursa;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtNume.Clear();
            txtLocatie.Clear();
            chkVIP.IsChecked = false;
            rbPublic.IsChecked = true;
            ResetareCulori();
        }

        private void ResetareCulori()
        {
            lblNume.Foreground = culoareVerde;
            lblLocatie.Foreground = culoareVerde;
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
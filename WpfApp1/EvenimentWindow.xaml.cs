using System;
using System.Windows;
using System.Windows.Controls;
using LibrarieModele; // Asigură-te că ai referința la librăria ta

namespace WpfApp1
{
    public partial class EvenimentWindow : Window
    {
        public EvenimentWindow()
        {
            InitializeComponent();
        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            // Validare minimă
            if (string.IsNullOrEmpty(txtNume.Text) || dpData.SelectedDate == null)
            {
                MessageBox.Show("Vă rugăm să introduceți numele și data!");
                return;
            }

            // Exemplu de creare obiect (presupunând că ai clasa Eveniment)
            string nume = txtNume.Text;
            string locatie = txtLocatie.Text;
            DateTime data = dpData.SelectedDate.Value;
            string tip = (cmbTip.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Aici poți apela metoda de salvare în fișier (AdministrareEvenimente.Add)
            MessageBox.Show($"Evenimentul '{nume}' a fost salvat cu succes!");
            this.Close();
        }

        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
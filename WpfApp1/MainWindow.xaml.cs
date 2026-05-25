using LibrarieModele; // Asigură-te că proiectul tău are referință către LibrarieModele
using NivelStocareDate; // Asigură-te că proiectul tău are referință către NivelStocareDate
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private Participant _participantNou = new Participant();
        public Participant ParticipantNou
        {
            get => _participantNou;
            set { _participantNou = value; OnPropertyChanged("ParticipantNou"); }
        }
        // 1. Declarația pentru stocarea în fișier (Rezolvă eroarea 'adminEvenimente' does not exist)
        IStocareData adminEvenimente = new AdministrareEvenimenteFisierText("evenimente.txt");

        private const int MAX_LUNGIME_NUME = 15;
        private readonly SolidColorBrush culoareVerde = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F5D50"));

        public MainWindow()
        {
            InitializeComponent();

            // Populare ListBox cu valorile din Enum
            lstTipuri.ItemsSource = Enum.GetValues(typeof(CategorieEveniment));
            this.DataContext = this; // Esențial pentru Binding
            // Încărcare inițială a datelor
            IncarcaDate();
        }

        // 2. Metoda de încărcare a datelor (Refresh UI)
        private void IncarcaDate()
        {
            var evenimente = adminEvenimente.GetEvenimente();

            // ListBox-ul principal de afișare
            lstEvenimente.ItemsSource = null;
            lstEvenimente.ItemsSource = evenimente.Select(e => e.Info()).ToList();

            // ComboBox-ul pentru selecție modificare
          //cmbEvenimenteModificare.ItemsSource = null;
            cmbEvenimenteModificare.ItemsSource = evenimente;
        }

        // 3. Adăugare Eveniment Nou
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (ValidareDate() == 0)
            {
                Eveniment evNou = new Eveniment();
                evNou.NumeEveniment = txtNume.Text;
                evNou.Locatie = txtLocatie.Text;
                evNou.Tip = (CategorieEveniment)lstTipuri.SelectedItem;
                evNou.DataEveniment = dtpDataEveniment.SelectedDate ?? DateTime.Now;

                adminEvenimente.AddEveniment(evNou);

                MessageBox.Show("Eveniment adăugat cu succes!");
                IncarcaDate();
                btnReset_Click(sender, e);
            }
        }

        // 4. Selecție din ComboBox (Rezolvă eroarea 'cmbEvenimenteModificare_SelectionChanged' missing)
        private void cmbEvenimenteModificare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificăm dacă obiectul selectat este un Eveniment
            if (cmbEvenimenteModificare.SelectedItem is Eveniment ev)
            {
                txtNume.Text = ev.NumeEveniment;
                txtLocatie.Text = ev.Locatie;
                lstTipuri.SelectedItem = ev.Tip;
                dtpDataEveniment.SelectedDate = ev.DataEveniment;
            }
        }

        // 5. Buton Actualizare (Tema de acasă)
        private void btnActualizeaza_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEvenimenteModificare.SelectedItem is Eveniment evSelectat)
            {
                evSelectat.NumeEveniment = txtNume.Text;
                evSelectat.Locatie = txtLocatie.Text;
                evSelectat.Tip = (CategorieEveniment)lstTipuri.SelectedItem;
                evSelectat.DataEveniment = dtpDataEveniment.SelectedDate ?? DateTime.Now;

                bool succes = adminEvenimente.UpdateEveniment(evSelectat);

                if (succes)
                {
                    MessageBox.Show("Modificarea a fost salvată!");
                    IncarcaDate();
                }
            }
            else
            {
                MessageBox.Show("Selectați un eveniment din listă pentru a-l modifica!");
            }
        }

        // 6. Căutare (Corectată să folosească fișierul, nu listaDate)
        private void txtCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textCautat = txtCautare.Text.ToLower();
            var rezultate = adminEvenimente.GetEvenimente()
                            .Where(ev => ev.NumeEveniment.ToLower().Contains(textCautat))
                            .Select(ev => ev.Info())
                            .ToList();

            lstEvenimente.ItemsSource = null;
            lstEvenimente.ItemsSource = rezultate;
        }

        private int ValidareDate()
        {
            int erori = 0;
            ResetareCulori();

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
            if (lstTipuri.SelectedItem == null)
            {
                MessageBox.Show("Selectați un tip de eveniment!");
                erori++;
            }

            return erori;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtNume.Clear();
            txtLocatie.Clear();
            lstTipuri.SelectedIndex = -1;
            dtpDataEveniment.SelectedDate = null;
            cmbEvenimenteModificare.SelectedIndex = -1;
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
        private void btnSalveazaParticipant_Click(object sender, RoutedEventArgs e)
        {
            // Datorită Binding-ului, obiectul ParticipantNou este deja populat
            adminEvenimente.
                AddParticipant(ParticipantNou);
            MessageBox.Show($"Salvat: {ParticipantNou.Nume}");

            // Resetăm obiectul pentru o nouă introducere
            ParticipantNou = new Participant();
        }

        // Implementare necesară pentru ca UI-ul să afle când se schimbă datele în cod
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
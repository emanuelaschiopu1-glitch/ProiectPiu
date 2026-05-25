using System;
using System.ComponentModel; // Necesar pentru IDataErrorInfo și INotifyPropertyChanged
using System.Runtime.CompilerServices; // Necesar pentru CallerMemberName

namespace LibrarieModele
{
    // Adăugăm interfețele: 
    // INotifyPropertyChanged = anunță interfața grafică (WPF) când se schimbă o valoare
    // IDataErrorInfo = permite validarea datelor direct în clasă
    public class Participant : INotifyPropertyChanged, IDataErrorInfo
    {
        private string nume;
        private string email;
        private int varsta;

        // Proprietăți "Full" cu notificare
        public string Nume
        {
            get => nume;
            set { nume = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public int Varsta
        {
            get => varsta;
            set { varsta = value; OnPropertyChanged(); }
        }

        public string Info()
        {
            return $"Participant: {Nume}, Email: {Email}, Varsta: {Varsta}";
        }

        // --- PARTEA DE VALIDARE (IDataErrorInfo) ---

        public string Error => null; // Nu se folosește de obicei la nivel de obiect întreg

        public string this[string columnName]
        {
            get
            {
                string result = null;

                // Aici scrii regulile de validare pentru fiecare câmp
                if (columnName == nameof(Nume))
                {
                    if (string.IsNullOrWhiteSpace(Nume))
                        result = "Numele nu poate fi gol!";
                    else if (Nume.Length < 3)
                        result = "Numele este prea scurt (minim 3 caractere)!";
                }

                if (columnName == nameof(Email))
                {
                    if (string.IsNullOrWhiteSpace(Email))
                        result = "Email-ul este obligatoriu!";
                    else if (!Email.Contains("@") || !Email.Contains("."))
                        result = "Format email invalid (lipsesc @ sau .)!";
                }

                if (columnName == nameof(Varsta))
                {
                    if (Varsta < 14 || Varsta > 100)
                        result = "Vârsta trebuie să fie între 14 și 100 ani!";
                }

                return result;
            }
        }

        // --- PARTEA DE NOTIFICARE (INotifyPropertyChanged) ---

        public event PropertyChangedEventHandler PropertyChanged;

        // Metodă ajutătoare care spune ferestrei: "Hei, valoarea proprietății X s-a schimbat, redesenează-mă!"
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibrarieModele; // Referință către proiectul cu Participant

namespace WpfApp1
{
    // ViewModel-ul trebuie și el să anunțe interfața când se schimbă ceva
    public class MainViewModel : INotifyPropertyChanged
    {
        private Participant _participantNou;

        public MainViewModel()
        {
            // Inițializăm un participant gol la pornire
            _participantNou = new Participant();
        }

        // Aceasta este proprietatea la care se va lega (Bind) interfața grafică
        public Participant ParticipantNou
        {
            get => _participantNou;
            set
            {
                _participantNou = value;
                OnPropertyChanged();
            }
        }

        // --- Logica de notificare ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
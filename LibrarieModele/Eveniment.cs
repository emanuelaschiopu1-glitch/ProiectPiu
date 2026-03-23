using System;

namespace LibrarieModele
{
    public class Eveniment
    {
        public int IdEveniment { get; set; }
        public string NumeEveniment { get; set; }
        public string TipEveniment { get; set; }
        public string Organizator { get; set; }
        public DateTime DataEveniment { get; set; }
        public string Locatie { get; set; }
        public int NrInvitati { get; set; }

        public Eveniment()
        {
            NumeEveniment = string.Empty;
            TipEveniment = string.Empty;
            Organizator = string.Empty;
            Locatie = string.Empty;
            DataEveniment = DateTime.Now;
            NrInvitati = 0;
        }

        public string Info()
        {
            return $"Id: {IdEveniment} | Nume: {NumeEveniment} | Tip: {TipEveniment} | Organizator: {Organizator} | " +
                   $"Data: {DataEveniment} | Locatie: {Locatie} | Invitati: {NrInvitati}";
        }
    }
}
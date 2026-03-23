using System;
using System.Linq;

namespace LibrarieModele
{

    public enum CategorieEveniment
    {
        Cultural = 1,
        Sportiv = 2,
        Stiintific = 3,
        Divertisment = 4
    }
    public class Eveniment
    {
        private int[] ratinguri;
        public int IdEveniment { get; set; }
        public string NumeEveniment { get; set; }
        public CategorieEveniment Tip{ get; set; }
        public string Organizator { get; set; }
        public DateTime DataEveniment { get; set; }
        public string Locatie { get; set; }
        public int NrInvitati { get; set; }

        //prop LINQ
        public double RatingMediu => (ratinguri != null && ratinguri.Any()) ? ratinguri.Average() : 0;


        public Eveniment()
        {
            NumeEveniment = string.Empty;
            Tip = CategorieEveniment.Divertisment;
            Organizator = string.Empty;
            Locatie = string.Empty;
            DataEveniment = DateTime.Now;
            NrInvitati = 0;
            ratinguri = new int[0];
        }

        public void SetRatinguri(int[] _ratinguri) => ratinguri = _ratinguri;
        public string Info()
        {
            return $"Id: {IdEveniment} | Nume: {NumeEveniment} | Tip: {Tip} | Organizator: {Organizator} | " +
                   $"Data: {DataEveniment.ToShortDateString()} | Locatie: {Locatie} | Invitati: {NrInvitati} | Rating: {RatingMediu:F2}";
        }
    }
}
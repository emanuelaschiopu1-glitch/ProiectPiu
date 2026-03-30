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
        //constante pt fisier.txt
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ',';
        private const bool SUCCES = true;


        //index pt coloane

        private const int ID = 0;
        private const int NUME = 1;
        private const int TIP = 2;
        private const int ORGANIZATOR = 3;
        //private const int DATA = 4;
        private const int LOCATIE = 4;
        private const int NR_INVITATI = 5;
        private const int NOTE_SAU_RATINGS = 6;

        // date membre
        private int[] ratinguri;

        //proprietati
        public int IdEveniment { get; set; }
        public string NumeEveniment { get; set; }
        public CategorieEveniment Tip { get; set; }
        public string Organizator { get; set; }
        //public DateTime DataEveniment { get; set; }
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
            //DataEveniment = DateTime.Now;
            NrInvitati = 0;
            ratinguri = new int[0];
        }

        // Constructor cu Parametri (folosit la citirea de la tastatura)
        public Eveniment(int id, string nume, string locatie)
        {
            IdEveniment = id;
            NumeEveniment = nume;
            Locatie = locatie;
            Tip = CategorieEveniment.Divertisment;
            Organizator = "Necunoscut";
            //DataEveniment = DateTime.Now;
            ratinguri = new int[0];
        }

        //constructor pt fisier
        public Eveniment(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            // Ordinea de preluare a campurilor conform stilului cerut
            this.IdEveniment = Convert.ToInt32(dateFisier[ID]);
            this.NumeEveniment = dateFisier[NUME];
            this.Tip = (CategorieEveniment)Enum.Parse(typeof(CategorieEveniment), dateFisier[TIP]);
            this.Organizator = dateFisier[ORGANIZATOR];
            //this.DataEveniment = DateTime.Parse(dateFisier[DATA]);
            this.Locatie = dateFisier[LOCATIE];
            this.NrInvitati = Convert.ToInt32(dateFisier[NR_INVITATI]);
            // APEL METODA EXTRAGERE (Esențial pentru a incarca notele/ratingurile)
           // ExtrageRatinguri(dateFisier[RATINGS], SEPARATOR_SECUNDAR_FISIER);
        }

            



        // Extrage ratinguri
        public void ExtrageRatinguri(string sirRatinguri, char delimitator = ',')
        {
            List<int> listaNote = new List<int>();

            if (!string.IsNullOrEmpty(sirRatinguri))
            {
                foreach (var element in sirRatinguri.Split(delimitator))
                {
                    if (int.TryParse(element, out int nota))
                    {
                        listaNote.Add(nota);
                    }
                }
            }
            ratinguri = listaNote.ToArray();
        }

        public void SetRatinguri(int[] _ratings)
        {
            if (_ratings != null)
                ratinguri = (int[])_ratings.Clone();
        }

        public int[] GetRatinguri() => (int[])(ratinguri?.Clone() ?? new int[0]);

        // Metoda pentru salvare in fisier (Stilul string.Format)
        public string ConversieLaSirPentruFisier()
        {
            string sRatings = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), ratinguri ?? new int[0]);

            // ATENTIE: Ordinea aici trebuie sa fie aceeasi cu indexurile de sus!
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdEveniment,
                NumeEveniment ?? "NECUNOSCUT",
                (int)Tip,
                Organizator ?? "NECUNOSCUT",
                //DataEveniment.ToString("yyyy-MM-dd"),
                Locatie ?? "NECUNOSCUT",
                NrInvitati,
                sRatings);
        }
        public string Info()
        {
            return $"Id: {IdEveniment} | Nume: {NumeEveniment} | Tip: {Tip} | Organizator: {Organizator} | Locatie: {Locatie} | Invitati: {NrInvitati} | Rating: {RatingMediu:F2}";
        }
    }
}
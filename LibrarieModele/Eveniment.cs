using System;
using System.Linq;
using System.Collections.Generic;
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
        private const int LOCATIE = 4;
        private const int NR_INVITATI = 5;
        private const int DATA_EVENIMENT = 6;
        private const int DATA_ACTUALIZARE = 7;
        private const int RATINGS = 8;

        // date membre
        private int[] ratinguri;

        //proprietati

        public List<Participant> Participanti { get; set; } = new List<Participant>();
        public int IdEveniment { get; set; }
        public string NumeEveniment { get; set; }
        public CategorieEveniment Tip { get; set; }
        public string Organizator { get; set; }
        
        public string Locatie { get; set; }
        public int NrInvitati { get; set; }

        public DateTime DataEveniment { get; set; }
        public DateTime DataActualizare { get; set; }

        //prop LINQ
        public double RatingMediu => (ratinguri != null && ratinguri.Any()) ? ratinguri.Average() : 0;


        /*public void AdaugaParticipant(Participant p)
        {
            Participanti.Add(p);
        }
        public string AfiseazaParticipanti()
        {
            if (Participanti.Count == 0)
                return "Nu exista participanti.";

            string rezultat = "Participanti:\n";

            foreach (var p in Participanti)
                rezultat += p.Info() + "\n";

            return rezultat;
        }*/
        public Eveniment()
        {
            NumeEveniment = string.Empty;
            Tip = CategorieEveniment.Divertisment;
            Organizator = string.Empty;
            Locatie = string.Empty;
            NrInvitati = 0;
            DataEveniment = DateTime.Now;
            DataActualizare = DateTime.Now;
            ratinguri = new int[0];
        }

        // Constructor cu Parametri (folosit la citirea de la tastatura)
       /* public Eveniment(int id, string nume, string locatie)
        {
            IdEveniment = id;
            NumeEveniment = nume;
            Locatie = locatie;
            Tip = CategorieEveniment.Divertisment;
            Organizator = "Necunoscut";
            //DataEveniment = DateTime.Now;
            ratinguri = new int[0];
        }*/

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
            this.DataEveniment = DateTime.Parse(dateFisier[DATA_EVENIMENT]);
            this.DataActualizare = DateTime.Parse(dateFisier[DATA_ACTUALIZARE]);

            if (dateFisier.Length > RATINGS)
                ExtrageRatinguri(dateFisier[RATINGS], SEPARATOR_SECUNDAR_FISIER);
        }

        public string ConversieLaSirPentruFisier()
        {
            string sRatings = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), ratinguri ?? new int[0]);

            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdEveniment,
                NumeEveniment ?? "NECUNOSCUT",
                (int)Tip,
                Organizator ?? "NECUNOSCUT",
                Locatie ?? "NECUNOSCUT",
                NrInvitati,
                DataEveniment.ToString("yyyy-MM-dd"), // Format pentru DatePicker
                DataActualizare.ToString("G"),        // Format complet Data+Ora
                sRatings);
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

        
        public string Info()
        {
            return $"Id: {IdEveniment} | Nume: {NumeEveniment} | Tip: {Tip} | Organizator: {Organizator} | Locatie: {Locatie} | Invitati: {NrInvitati} | Rating: {RatingMediu:F2}";
        }
    }
}
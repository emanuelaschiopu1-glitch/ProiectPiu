using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele; // Referință către proiectul de modele

namespace NivelStocareDate
{
    public class AdministrareEvenimenteFisierText : IStocareData
    {
        private const int ID_PRIMUL_EVENIMENT = 1;
        private const int INCREMENT = 1;
        private string numeFisier;

        public AdministrareEvenimenteFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // Creează fișierul automat la prima rulare dacă nu există
            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Dispose();
            }
        }

        // Adăugare Eveniment (Scriere)
        public void AddEveniment(Eveniment ev)
        {
            ev.IdEveniment = GetNextId();
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(ev.ConversieLaSirPentruFisier());
            }
        }

        // Preluare toate evenimentele (Citire)
        public List<Eveniment> GetEvenimente()
        {
            List<Eveniment> lista = new List<Eveniment>();
            if (!File.Exists(numeFisier)) return lista;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linie))
                    {
                        // Aici Eveniment trebuie să aibă un constructor care primește string
                        lista.Add(new Eveniment(linie));
                    }
                }
            }
            return lista;
        }

        // Generare ID Incremental
        private int GetNextId()
        {
            var lista = GetEvenimente();
            if (lista.Count == 0) return ID_PRIMUL_EVENIMENT;
            return lista.Last().IdEveniment + INCREMENT;
        }

        // --- GESTIUNE PARTICIPANȚI (Opțional în același fișier sau altul) ---
        private string numeFisierParticipanti = "participanti.txt";

        public void AddParticipant(Participant p)
        {
            using (StreamWriter sw = new StreamWriter(numeFisierParticipanti, true))
            {
                sw.WriteLine($"{p.Nume};{p.Email};{p.Varsta}");
            }
        }

        // Poți adăuga și restul metodelor (Delete, Update) aici...
        // Adaugă aceste metode în AdministrareEvenimenteFisierText.cs
        public Eveniment GetEveniment(string nume) => null;
        public bool UpdateEveniment(Eveniment evModificat) => false;
        public List<Participant> GetParticipanti() => new List<Participant>();
        public bool DeleteParticipant(string email) => false;
    }
}
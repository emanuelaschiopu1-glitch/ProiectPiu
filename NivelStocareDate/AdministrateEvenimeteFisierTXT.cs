using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

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
            // Creează fișierul dacă nu există
            Stream s = File.Open(numeFisier, FileMode.OpenOrCreate);
            s.Close();
        }


        //scrierea in fisier
        public void AddEveniment(Eveniment ev)
        {
            ev.IdEveniment = GetNextId();

            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(ev.ConversieLaSirPentruFisier());
            }
        }


        //citirea
        public List<Eveniment> GetEvenimente()
        {
            List<Eveniment> lista = new List<Eveniment>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                // Citim linie cu linie până la final
                while ((linie = sr.ReadLine()) != null)
                {
                    lista.Add(new Eveniment(linie));
                }
            }
            return lista;
        }

        public Eveniment GetEveniment(string nume)
        {
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    Eveniment ev = new Eveniment(linie);
                    if (ev.NumeEveniment.Equals(nume, StringComparison.OrdinalIgnoreCase))
                        return ev;
                }
            }
            return null;
        }


        // modificarea datelor 
        public bool UpdateEveniment(Eveniment evModificat)
        {
            List<Eveniment> lista = GetEvenimente();
            bool succes = false;

       
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Eveniment ev in lista)
                {
                    Eveniment deScris = ev;
                    if (ev.IdEveniment == evModificat.IdEveniment)
                    {
                        deScris = evModificat;
                        succes = true;
                    }
                    sw.WriteLine(deScris.ConversieLaSirPentruFisier());
                }
            }
            return succes;
        }

        private int GetNextId()
        {
            List<Eveniment> lista = GetEvenimente();
            if (lista.Count == 0)
            {
                return ID_PRIMUL_EVENIMENT;
            }
            return lista.Last().IdEveniment + INCREMENT; // Atenție aici la numele proprietății (IdEveniment)
        }

        private string numeFisierParticipanti = "participanti.txt";

        public void AddParticipant(Participant p)
        {
            using (StreamWriter sw = new StreamWriter(numeFisierParticipanti, true))
            {
                // Format: Nume;Email;Varsta
                sw.WriteLine($"{p.Nume};{p.Email};{p.Varsta}");
            }
        }

        public List<Participant> GetParticipanti()
        {
            List<Participant> lista = new List<Participant>();
            if (!File.Exists(numeFisierParticipanti)) return lista;

            foreach (var linie in File.ReadAllLines(numeFisierParticipanti))
            {
                var date = linie.Split(';');
                if (date.Length == 3)
                    lista.Add(new Participant { Nume = date[0], Email = date[1], Varsta = int.Parse(date[2]) });
            }
            return lista;
        }

        public bool DeleteParticipant(string email)
        {
            var participanti = GetParticipanti();
            var deSters = participanti.FirstOrDefault(p => p.Email == email);
            if (deSters != null)
            {
                participanti.Remove(deSters);
                File.WriteAllLines(numeFisierParticipanti, participanti.Select(p => $"{p.Nume};{p.Email};{p.Varsta}"));
                return true;
            }
            return false;
        }
    }
}
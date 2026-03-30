using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareEvenimenteFisierText : IStocareData
    {
        private string numeFisier;
        private string numeFisierOrgs = "Organizatori.txt";

        public AdministrareEvenimenteFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream s = File.Open(numeFisier, FileMode.OpenOrCreate);
            s.Close();
        }

        public void AddEveniment(Eveniment ev)
        {
            ev.IdEveniment = GetNextId();
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(ev.ConversieLaSirPentruFisier());
            }
        }

        public List<Eveniment> GetEvenimente()
        {
            List<Eveniment> lista = new List<Eveniment>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    lista.Add(new Eveniment(linie));
                }
            }
            return lista;
        }

        public Eveniment GetEveniment(string nume)
        {
            // Folosim metoda GetEvenimente() nu o variabila locala!
            return GetEvenimente().FirstOrDefault(e => e.NumeEveniment.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateEveniment(Eveniment evModificat)
        {
            List<Eveniment> lista = GetEvenimente();
            bool succes = false;
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (var e in lista)
                {
                    if (e.IdEveniment == evModificat.IdEveniment)
                    {
                        sw.WriteLine(evModificat.ConversieLaSirPentruFisier());
                        succes = true;
                    }
                    else sw.WriteLine(e.ConversieLaSirPentruFisier());
                }
            }
            return succes;
        }

        private int GetNextId()
        {
            var lista = GetEvenimente();
            return (lista.Count == 0) ? 1 : lista.Max(e => e.IdEveniment) + 1;
        }

        /* public void AddOrganizator(Organizator org)
        {
            using (StreamWriter sw = new StreamWriter(numeFisierOrgs, true))
            {
                sw.WriteLine(org.ConversieLaSirPentruFisier());
            }
        }

        *public List<Organizator> GetOrganizatori()
        {
            List<Organizator> lista = new List<Organizator>();
            if (!File.Exists(numeFisierOrgs)) return lista;
            using (StreamReader sr = new StreamReader(numeFisierOrgs))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null) { lista.Add(new Organizator(linie)); }
            }
            return lista;
        }*/
    }
}
using System;
using System.Collections.Generic;
using LibrarieModele;
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareEvenimenteMemorie : IStocareData
    {
        private List<Eveniment> evenimente;

        public AdministrareEvenimenteMemorie()
        {
            evenimente = new List<Eveniment>();
        }

        public void AddEveniment(Eveniment ev)
        {
            ev.IdEveniment = (evenimente.Count == 0) ? 1 : evenimente.Max(e => e.IdEveniment) + 1;
            evenimente.Add(ev);
        }

        public List<Eveniment> GetEvenimente() => evenimente;

        public Eveniment GetEveniment(string nume) =>
            evenimente.FirstOrDefault(e => e.NumeEveniment.Equals(nume, StringComparison.OrdinalIgnoreCase));

        public List<Eveniment> GetEvenimenteDupaCategorie(CategorieEveniment categorie)
        {
            
            return evenimente.Where(e => e.Tip == categorie).ToList();
        }
        public List<Eveniment> GetEvenimenteNume(string nume) =>
            evenimente.Where(e => e.NumeEveniment.Contains(nume, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
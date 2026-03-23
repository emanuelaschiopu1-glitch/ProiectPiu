using System.Collections.Generic;
using System.Linq;
using LibrarieModele;

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
            ev.IdEveniment = (evenimente.Count == 0) ? 1 : evenimente.Last().IdEveniment + 1;
            evenimente.Add(ev);
        }

        public List<Eveniment> GetEvenimente() => evenimente;

        public Eveniment GetEveniment(string nume) =>
            evenimente.FirstOrDefault(e => e.NumeEveniment.Equals(nume, System.StringComparison.OrdinalIgnoreCase));

        public List<Eveniment> GetEvenimenteTip(string tip) =>
            evenimente.Where(e => e.TipEveniment.Equals(tip, System.StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
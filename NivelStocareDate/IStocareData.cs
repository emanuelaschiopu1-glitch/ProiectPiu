using System.Collections.Generic;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        void AddEveniment(Eveniment ev);
        List<Eveniment> GetEvenimente();
        Eveniment GetEveniment(string nume);
        bool UpdateEveniment(Eveniment evModificat);
    }
}
using System.Collections.Generic;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareData
    {
        // --- CRUD Eveniment ---
        void AddEveniment(Eveniment ev);
        List<Eveniment> GetEvenimente();
        Eveniment GetEveniment(string nume);
        bool UpdateEveniment(Eveniment evModificat);

        // --- CRUD Participant (A doua entitate) ---
        void AddParticipant(Participant p);        // Create
        List<Participant> GetParticipanti();       // Read (Lipsește în codul tău)
        bool DeleteParticipant(string email);      // Delete
    }
}
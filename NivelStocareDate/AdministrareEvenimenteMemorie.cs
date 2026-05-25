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

        
        
        

        // Metoda pentru cautare dupa nume
        public Eveniment GetEveniment(string nume)
        {
            return evenimente.FirstOrDefault(e => e.NumeEveniment.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }



        // Metoda pentru modificare
        public bool UpdateEveniment(Eveniment evModificat)
        {
            var index = evenimente.FindIndex(e => e.IdEveniment == evModificat.IdEveniment);
            if (index != -1)
            {
                evenimente[index] = evModificat;
                return true;
            }
            return false;
        }
        // Adaugă aceste metode în AdministrareEvenimenteMemorie.cs
        public void AddParticipant(Participant p)
        {
            // Opțional: poți adăuga o listă privată de participanți aici
        }

        public List<Participant> GetParticipanti()
        {
            return new List<Participant>();
        }

        public bool DeleteParticipant(string email)
        {
            return false;
        }
    }

}
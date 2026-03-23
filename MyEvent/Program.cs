using System;
using System.Collections.Generic;
using LibrarieModele;
using NivelStocareDate;

namespace EvidentaEvenimente
{
    class Program
    {
        static void Main(string[] args)
        {
            IStocareData admin = StocareFactory.GetAdministratorStocare();
            Eveniment evNou = null;
            string optiune;

            do
            {
                Console.WriteLine("\n--- GESTIUNE EVENIMENTE ---");
                Console.WriteLine("C. Citire date (manual)");
                Console.WriteLine("S. Salvare in lista");
                Console.WriteLine("A. Afisare toate");
                Console.WriteLine("F. Cauta dupa nume");
                Console.WriteLine("X. Iesire");
                Console.Write("Optiune: ");

                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        evNou = CitireTastatura();
                        break;
                    case "S":
                        if (evNou != null)
                        {
                            admin.AddEveniment(evNou);
                            Console.WriteLine("Salvat cu succes!");
                            evNou = null;
                        }
                        else Console.WriteLine("Eroare: Cititi datele mai intai!");
                        break;
                    case "A":
                        var lista = admin.GetEvenimente();
                        if (lista.Count > 0)
                            foreach (var e in lista) Console.WriteLine(e.Info());
                        else Console.WriteLine("Lista este goala.");
                        break;
                    case "F":
                        Console.Write("Nume de cautat: ");
                        string nume = Console.ReadLine();
                        var gasit = admin.GetEveniment(nume);
                        Console.WriteLine(gasit != null ? gasit.Info() : "Negasit.");
                        break;
                }
            } while (optiune != "X");
        }

        static Eveniment CitireTastatura()
        {
            Console.WriteLine("\n--- Introducere manuala date ---");

            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Tip: "); string tip = Console.ReadLine();
            Console.Write("Organizator: "); string organizator = Console.ReadLine();
            Console.Write("Locatie: "); string locatie = Console.ReadLine();

            Console.Write("Nr. Invitati: ");
            int.TryParse(Console.ReadLine(), out int nr);

            // CITIERE MANUALA DATA
            Console.Write("Data (format AN-LUNA-ZI, ex: 2026-05-20): ");
            string dataCitita = Console.ReadLine();

            // Inceracam sa convertim ce a scris utilizatorul. 
            // Daca nu reuseste, va pune data de azi ca "siguranta".
            DateTime dataFinala;
            if (!DateTime.TryParse(dataCitita, out dataFinala))
            {
                dataFinala = DateTime.Now;
                Console.WriteLine("Format data invalid! S-a folosit data curenta.");
            }

            return new Eveniment
            {
                NumeEveniment = nume,
                TipEveniment = tip,
                Organizator = organizator,
                Locatie = locatie,
                NrInvitati = nr,
                DataEveniment = dataFinala // Aici punem data aleasa de tine
            };
        }
    }
}
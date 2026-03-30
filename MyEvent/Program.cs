using System;
using System.Collections.Generic;
using LibrarieModele;
using NivelStocareDate;
using System.Linq;

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
           // Console.Write("Tip: "); string tip = Console.ReadLine();
            Console.Write("Organizator: "); string organizator = Console.ReadLine();
            Console.Write("Locatie: "); string locatie = Console.ReadLine();


            CategorieEveniment tipSelectat = CategorieEveniment.Divertisment;
            bool tipValid = false;
            while (!tipValid)
            {
                try
                {
                    Console.WriteLine("Tipuri disponibile: 1-Cultural, 2-Sportiv, 3-Stiintific, 4-Divertisment");
                    Console.Write("Alegeti tipul (cifra): ");
                    int optiuneTip = int.Parse(Console.ReadLine()); // Aruncă FormatException dacă nu e cifră

                    if (Enum.IsDefined(typeof(CategorieEveniment), optiuneTip))
                    {
                        tipSelectat = (CategorieEveniment)optiuneTip;
                        tipValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Eroare: Cifra introdusa nu este in lista.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Introdu rating-uri de la participanti (ex: 4 5 3 5):");
                }
            }

            Console.Write("Nr. Invitati: ");
            int.TryParse(Console.ReadLine(), out int nr);

            

       
           

            Console.Write("Introdu rating-uri de la participanti (ex: 4 5 3 5): ");
            string input = Console.ReadLine() ?? "";
            int[] ratings = input.Split(' ')
                     .Where(s => !string.IsNullOrWhiteSpace(s)) //elimina spatiile libere
                     .Select(n => int.TryParse(n, out int r) ? r : 0)
                     .ToArray(); //rez corect 

            Eveniment ev = new Eveniment
            {
                NumeEveniment = nume,
                Tip = tipSelectat,
                Organizator = organizator,
                Locatie = locatie,
                NrInvitati = nr,
              
            };
            ev.SetRatinguri(ratings);
            return ev;
        }
    }
}
using System;

namespace LibrarieModele
{
    public class Participant
    {
        public string Nume { get; set; }
        public string Email { get; set; }
        public int Varsta { get; set; }

        public string Info()
        {
            return $"Participant: {Nume}, Email: {Email}, Varsta: {Varsta}";
        }
    }
}
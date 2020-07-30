using Prodavnica_Racunara.Enums;
using System;

namespace Prodavnica_Racunara.Models
{
    class Korisnik
    {
        public Korisnik(string data)
        {
            string[] podaci = data.Split(';');
            Username = podaci[0];
            Lozinka = podaci[1];
            Ime = podaci[2];
            Prezime = podaci[3];
            Enum.TryParse(podaci[4], out Uloga);
        }

        public string Username;
        public string Lozinka;
        public string Ime;
        public string Prezime;
        public AccountRole Uloga;

        public string Save()
        {
            string data = Username + ";" + Lozinka + ";" + Ime + ";" + Prezime + ";" + Uloga.ToString();
            return data;
        }
    }
}

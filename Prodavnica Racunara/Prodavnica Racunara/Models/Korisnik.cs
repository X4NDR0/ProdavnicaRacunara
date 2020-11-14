using Prodavnica_Racunara.Enums;
using System;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class
    /// </summary>
    public class Korisnik
    {
        /// <summary>
        /// Representing constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        public Korisnik(string data)
        {
            string[] podaci = data.Split(';');
            Username = podaci[0];
            Lozinka = podaci[1];
            Ime = podaci[2];
            Prezime = podaci[3];
            Enum.TryParse(podaci[4], out Uloga);
        }

        /// <summary>
        /// Representing username of the class
        /// </summary>
        public string Username;

        /// <summary>
        /// Representing password of the class
        /// </summary>
        public string Lozinka;

        /// <summary>
        /// Representing name of the class
        /// </summary>
        public string Ime;

        /// <summary>
        /// Representing seller surname of the class
        /// </summary>
        public string Prezime;

        /// <summary>
        /// Representing account role of the class
        /// </summary>
        public AccountRole Uloga;

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string data = Username + ";" + Lozinka + ";" + Ime + ";" + Prezime + ";" + Uloga.ToString();
            return data;
        }
    }
}

using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Utils;
using System;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing "Artical" class
    /// </summary>
    class Artikal
    {
        /// <summary>
        /// Representing empty constructor of the class
        /// </summary>
        public Artikal()
        {

        }

        /// <summary>
        /// Representing class constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        public Artikal(string data)
        {
            string[] podaci = data.Split(';');

            if (podaci.Length != 6)
            {
                Console.WriteLine("Error while reading file!");
            }
            else
            {
                
                int.TryParse(podaci[0], out Sifra);
                Naziv = podaci[1];
                double.TryParse(podaci[2], out Cena);
                int.TryParse(podaci[3], out Kolicina);
                Opis = podaci[4];
                Enum.TryParse(podaci[5], out Status);
            }
        }

        /// <summary>
        /// Representing ID of the artical
        /// </summary>
        public int Sifra;

        /// <summary>
        /// Representing Name of the artical
        /// </summary>
        public string Naziv;

        /// <summary>
        /// Representing price of the artical
        /// </summary>
        public double Cena;

        /// <summary>
        /// Representing quantity of the artical
        /// </summary>
        public int Kolicina;

        /// <summary>
        /// Representing "status" of the artical
        /// </summary>
        public Status Status;

        /// <summary>
        /// Representing description of the artical
        /// </summary>
        public string Opis;

        /// <summary>
        /// Representing class method for saving data to csv file
        /// </summary>
        /// <returns></returns>
        public virtual string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina  + ";" + Opis + ";" + Status.ToString();
            return data;
        }
    }
}

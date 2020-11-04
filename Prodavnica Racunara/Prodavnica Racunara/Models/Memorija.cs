using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class which inherits class component
    /// </summary>
    class Memorija : Komponenta
    {
        /// <summary>
        /// Representing emtpy constructor of the class
        /// </summary>
        public Memorija()
        {

        }

        /// <summary>
        /// Representing constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaKategorija"></param>
        public Memorija(string data, List<Kategorija> listaKategorija)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);
            Kategorija = listaKategorija.Where(x => x.Sifra.ToString().Equals(podaci[6])).FirstOrDefault();
            int.TryParse(podaci[7], out Kapacitet);
        }

        /// <summary>
        /// Representing object category of the memory
        /// </summary>
        public Kategorija Kategorija;

        /// <summary>
        /// Representing memory size of the memory
        /// </summary>
        public int Kapacitet;

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public override string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + Kategorija.Sifra + ";" + Kapacitet;
            return data;
        }

    }
}

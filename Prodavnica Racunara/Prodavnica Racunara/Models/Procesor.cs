using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class Procesor which inherits class component
    /// </summary>
    class Procesor : Komponenta
    {
        /// <summary>
        /// Representing empty class constructor
        /// </summary>
        public Procesor()
        {

        }

        /// <summary>
        /// Representing constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaKategorija"></param>
        public Procesor(string data, List<Kategorija> listaKategorija)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);
            Kategorija kategorija = listaKategorija.Where(x => x.Sifra == Convert.ToInt32(podaci[6])).FirstOrDefault();
            Kategorija = kategorija;
            double.TryParse(podaci[7], out RadniTakt);
            int.TryParse(podaci[8], out BrojJezgra);
        }


        /// <summary>
        /// Representing category of the procesor
        /// </summary>
        public Kategorija Kategorija;

        /// <summary>
        /// Representing frenquency of the procesor
        /// </summary>
        public double RadniTakt;

        /// <summary>
        /// Representing core number of the procesor
        /// </summary>
        public int BrojJezgra;

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public override string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + Kategorija.Sifra + ";" + RadniTakt + ";" + BrojJezgra;
            return data;
        }
    }
}

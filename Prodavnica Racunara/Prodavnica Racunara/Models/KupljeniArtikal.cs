using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class "KupljeniArtikal"
    /// </summary>
    class KupljeniArtikal
    {
        /// <summary>
        /// Representing emtpy constructor of the class
        /// </summary>
        public KupljeniArtikal()
        {

        }

        /// <summary>
        /// Representing constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaArtikal"></param>
        public KupljeniArtikal(string data,List<Artikal> listaArtikal)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out ID);
            Artikal artikal = listaArtikal.Where(x => x.Sifra == Convert.ToInt32(podaci[1])).FirstOrDefault();
            if (artikal != null)
            {
                Artikal = artikal;
            }
            int.TryParse(podaci[2], out Kolicina);
            double.TryParse(podaci[3], out Cena);
            double.TryParse(podaci[4], out UkupnaCena);
        }

        /// <summary>
        /// Representing ID of the "KupljeniArtikal"
        /// </summary>
        public int ID;

        /// <summary>
        /// Representing object Artikal ID of the "KupljeniArtikal"
        /// </summary>
        public Artikal Artikal;

        /// <summary>
        /// Representing quantity of the "KupljeniArtikal"
        /// </summary>
        public int Kolicina;

        /// <summary>
        /// Representing price of the "KupljeniArtikal"
        /// </summary>
        public double Cena;


        /// <summary>
        /// Representing full price of the "KupljeniArtikal"
        /// </summary>
        public double UkupnaCena;

        /// <summary>
        /// Representing class method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string data = ID + ";" + Artikal.Sifra + ";" + Kolicina + ";" + Cena + ";" + UkupnaCena;
            return data;
        }
    }
}

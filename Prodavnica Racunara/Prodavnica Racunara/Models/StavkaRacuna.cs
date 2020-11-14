using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class
    /// </summary>
    public class StavkaRacuna
    {
        /// <summary>
        /// Representing class with the parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaKupljenihArtikala"></param>
        public StavkaRacuna(string data, List<KupljeniArtikal> listaKupljenihArtikala)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            KupljeniArtikal kupljeniArtikal = listaKupljenihArtikala.Where(x => x.ID == Convert.ToInt32(podaci[1])).FirstOrDefault();
            if (kupljeniArtikal != null)
            {
                ProdatArtikal = kupljeniArtikal;
            }
            int.TryParse(podaci[2], out Kolicina);
            double.TryParse(podaci[3], out Cena);
        }

        /// <summary>
        /// Representing emtpy class constructor
        /// </summary>
        public StavkaRacuna()
        {

        }

        /// <summary>
        /// Representing ID of the class
        /// </summary>
        public int Sifra;

        /// <summary>
        /// Representing selled item of the class
        /// </summary>
        public KupljeniArtikal ProdatArtikal;

        /// <summary>
        /// Representing quantity of the class
        /// </summary>
        public int Kolicina;

        /// <summary>
        /// Representing price of the class
        /// </summary>
        public double Cena;

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string data = Sifra + ";" + ProdatArtikal.ID + ";" + Kolicina + ";" + Cena;
            return data;
        }
    }
}

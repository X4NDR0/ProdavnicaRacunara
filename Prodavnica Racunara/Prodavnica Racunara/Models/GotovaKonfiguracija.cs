using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class of "PC Configuration" which inherits class Artical
    /// </summary>
    class GotovaKonfiguracija : Artikal
    {
        /// <summary>
        /// Empty class contructor
        /// </summary>
        public GotovaKonfiguracija()
        {

        }


        /// <summary>
        /// Class constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaArtikla"></param>
        public GotovaKonfiguracija(string data, List<Artikal> listaArtikla)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);

            string[] idOfTheComponents = podaci[6].Split(",");

            List<Artikal> listaKomponenataAdd = new List<Artikal>(); ;

            for (int i = 0; i < idOfTheComponents.Length; i++)
            {
                Artikal komponentaLoad = listaArtikla.Where(x => x.Sifra == Convert.ToInt32(idOfTheComponents[i])).FirstOrDefault();

                if (komponentaLoad != null)
                {
                    listaKomponenataAdd.Add(komponentaLoad);
                }
                else
                {
                    Console.WriteLine("Error while adding component!");
                }
            }

            ListaKomponenata = listaKomponenataAdd;
        }

        /// <summary>
        /// Representing List of the articals
        /// </summary>
        public List<Artikal> ListaKomponenata;

        /// <summary>
        /// Representing method for saving data in csv file
        /// </summary>
        /// <returns></returns>
        public override string Save()
        {
            string data = string.Empty;
            string id = string.Empty;

            for (int i = 0; i < ListaKomponenata.Count; i++)
            {
                id += ListaKomponenata[i].Sifra + ",";
            }

            if (id.EndsWith(","))
            {
                id = id.Remove(id.Length - 1, 1);
            }

            data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + id;

            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class racun
    /// </summary>
    public class Racun
    {
        /// <summary>
        /// Representing class constructor of the class 
        /// </summary>
        public Racun()
        {

        }

        /// <summary>
        /// Representing class constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listaStavkiRacunaG"></param>
        public Racun(string data, List<StavkaRacuna> listaStavkiRacunaG)
        {
            string[] podaci = data.Split(";");
            int.TryParse(podaci[0], out Sifra);
            ImeProdavca = podaci[1];
            PrezimeProdavca = podaci[2];
            DateTime.TryParse(podaci[3], out Vreme);
            double.TryParse(podaci[4], out UkupnaCena);

            StavkaRacuna stavkaRacuna = listaStavkiRacunaG.Where(x => x.Sifra == Convert.ToInt32(podaci[5])).FirstOrDefault();
            ListaStavkiRacuna.Add(stavkaRacuna);
        }

        /// <summary>
        /// Representing ID of the class
        /// </summary>
        public int Sifra;

        /// <summary>
        /// Representing seller name of the class
        /// </summary>
        public string ImeProdavca;

        /// <summary>
        /// Representing seller surname of the class
        /// </summary>
        public string PrezimeProdavca;

        /// <summary>
        /// Representing date of the class
        /// </summary>
        public DateTime Vreme;

        /// <summary>
        /// Representing full price of the class
        /// </summary>
        public double UkupnaCena;

        /// <summary>
        /// Representing item list of the class
        /// </summary>
        public List<StavkaRacuna> ListaStavkiRacuna = new List<StavkaRacuna>();

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string data = Sifra + ";" + ImeProdavca + ";" + PrezimeProdavca + ";" + Vreme.ToString() + ";" + UkupnaCena;
            foreach (StavkaRacuna stavkaRacuna in ListaStavkiRacuna)
            {
                data += ";" + stavkaRacuna.Sifra;
            }
            return data;
        }

    }
}

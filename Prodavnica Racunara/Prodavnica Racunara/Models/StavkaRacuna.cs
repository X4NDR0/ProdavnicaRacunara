using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class StavkaRacuna
    {
        public StavkaRacuna(string data, List<KupljeniArtikal> listaKupljenihArtikala)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            KupljeniArtikal kupljeniArtikal = listaKupljenihArtikala.Where(x => x.ID == Convert.ToInt32(podaci[1])).FirstOrDefault();
            int.TryParse(podaci[2], out Kolicina);
            double.TryParse(podaci[3], out Cena);
        }

        public StavkaRacuna()
        {

        }

        public int Sifra;
        public KupljeniArtikal ProdatArtikal;
        public int Kolicina;
        public double Cena;

        public string Save()
        {
            string data = Sifra + ";" + ProdatArtikal.ID + ";" + Kolicina + ";" + Cena;
            return data;
        }
    }
}

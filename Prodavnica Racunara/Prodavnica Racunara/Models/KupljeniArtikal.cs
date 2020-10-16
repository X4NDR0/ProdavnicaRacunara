using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class KupljeniArtikal
    {
        public KupljeniArtikal(string data,List<Artikal> listaArtikal)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out ID);
            Artikal artikal = listaArtikal.Where(x => x.Naziv == podaci[1]).FirstOrDefault();
            int.TryParse(podaci[2], out Kolicina);
            double.TryParse(podaci[3], out Cena);
            double.TryParse(podaci[4], out UkupnaCena);
        }

        public KupljeniArtikal()
        {

        }

        public int ID;
        public Artikal Artikal;
        public int Kolicina;
        public double Cena;
        public double UkupnaCena;

        public string Save()
        {
            string data = Artikal.Sifra + ";" + Artikal.Naziv + ";" + Kolicina + ";" + Cena + ";" + UkupnaCena;
            return data;
        }
    }
}

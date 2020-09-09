using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class Memorija : Komponenta
    {
        public Memorija()
        {

        }

        public Memorija(string data, List<Kategorija> listaKategorija)
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
            int.TryParse(podaci[7], out Kapacitet);
        }

        public int Kapacitet;

        public override string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + Kategorija.Sifra + ";" + Kapacitet;
            return data;
        }

    }
}

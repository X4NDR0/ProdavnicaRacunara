using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class Procesor : Komponenta
    {
        public Procesor()
        {

        }

        public Procesor(string data, List<Kategorija> listaKategorija)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);
            Kategorija = listaKategorija.Where(x => x.kategorijaEnum.ToString().Equals(podaci[6])).FirstOrDefault();
            double.TryParse(podaci[7], out RadniTakt);
            int.TryParse(podaci[8], out BrojJezgra);
        }

        public Kategorija Kategorija;
        public double RadniTakt;
        public int BrojJezgra;

        public override string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + Kategorija.kategorijaEnum + ";" + RadniTakt + ";" + BrojJezgra;
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            Kategorija kategorija = listaKategorija.Where(x => x.Sifra == Convert.ToInt32(podaci[6])).FirstOrDefault();
            Kategorija = kategorija;
            double.TryParse(podaci[7], out RadniTakt);
            int.TryParse(podaci[8], out BrojJezgra);
        }

        public double RadniTakt;
        public int BrojJezgra;

        public string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + Kategorija.Sifra + ";" + RadniTakt + ";" + BrojJezgra;
            return data;
        }
    }
}

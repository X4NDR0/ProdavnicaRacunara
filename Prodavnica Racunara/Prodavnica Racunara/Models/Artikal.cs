using System;
using System.Collections.Generic;
using System.Text;
using Prodavnica_Racunara.Enums;

namespace Prodavnica_Racunara.Models
{
    class Artikal
    {
        public Artikal()
        {

        }
        public Artikal(string data)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);
        }

        public int Sifra;
        public string Naziv;
        public double Cena;
        public int Kolicina;
        public Status Status;
        public string Opis;

        public string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Status.ToString() + ";" + Opis;
            return data;
        }
    }
}

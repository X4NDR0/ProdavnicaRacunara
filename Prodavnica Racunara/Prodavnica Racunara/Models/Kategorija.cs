using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Utils;
using System;

namespace Prodavnica_Racunara.Models
{
    class Kategorija
    {
        public Kategorija()
        {

        }

        public Kategorija(string data)
        {
            string[] podaci = data.Split(';');
            Enum.TryParse(podaci[0], out kategorijaEnum);
            Opis = podaci[1];
        }

        public Kategorije kategorijaEnum;
        public string Opis;

        public string Save()
        {
            string data = kategorijaEnum + ";" + Opis;
            return data;
        }
    }
}

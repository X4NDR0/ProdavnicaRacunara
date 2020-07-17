using System;
using System.Collections.Generic;
using System.Text;

namespace Prodavnica_Racunara.Models
{
    class Komponenta : Artikal
    {
        public Kategorija Kategorija;

        public string Save()
        {
            string data = Kategorija.Sifra.ToString();
            return data;
        }
    }
}

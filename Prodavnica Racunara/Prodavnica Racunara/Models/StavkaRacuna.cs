using System;
using System.Collections.Generic;
using System.Text;

namespace Prodavnica_Racunara.Models
{
    class StavkaRacuna : Racun
    {
        public int RedniBroj;
        public Artikal ProdatArtikal;
        public double Cena;
        public int Kolicina;
    }
}

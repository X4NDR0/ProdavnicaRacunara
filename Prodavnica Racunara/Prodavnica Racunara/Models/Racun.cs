using System;
using System.Collections.Generic;
using System.Text;

namespace Prodavnica_Racunara.Models
{
    class Racun
    {
        public int Sifra;
        public string ImeProdavca;
        public string PrezimeProdavca;
        public DateTime Vreme;
        public double UkupnaCena;
        public List<StavkaRacuna> listaStavkiRacuna;
    }
}

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class Racun
    {

        public Racun(string data, List<Artikal> listaArtikala)
        {
            string[] podaci = data.Split(";");
            int.TryParse(podaci[0], out Sifra);
            ImeProdavca = podaci[1];
            PrezimeProdavca = podaci[2];
            DateTime.TryParse(podaci[3], out Vreme);
            double.TryParse(podaci[4], out UkupnaCena);

            string[] articalIds = podaci[5].Split(',');

            for (int i = 0; i < articalIds.Length; i++)
            {
                Artikal artikalLoad = listaArtikala.Where(x => x.Sifra == Convert.ToInt32(articalIds[i])).FirstOrDefault();
                if (artikalLoad != null)
                {
                    
                }
            }

        }

        public Racun()
        {

        }

        public int Sifra;
        public string ImeProdavca;
        public string PrezimeProdavca;
        public DateTime Vreme;
        public double UkupnaCena;
        public List<StavkaRacuna> listaStavkiRacuna;

        public string Save()
        {
            string data = Sifra + ";" + ImeProdavca + ";" + PrezimeProdavca + ";" + Vreme.ToString() + ";" + UkupnaCena;
            foreach (StavkaRacuna stavkaRacuna in listaStavkiRacuna)
            {
                data += ";" + stavkaRacuna.ProdatArtikal.Artikal.Sifra;
            }
            return data;
        }

    }
}

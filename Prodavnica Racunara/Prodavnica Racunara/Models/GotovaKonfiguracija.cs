using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Prodavnica_Racunara.Models
{
    class GotovaKonfiguracija : Artikal
    {
        public GotovaKonfiguracija()
        {

        }

        public GotovaKonfiguracija(string data, List<Artikal> listaArtikla)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            double.TryParse(podaci[2], out Cena);
            int.TryParse(podaci[3], out Kolicina);
            Opis = podaci[4];
            Enum.TryParse(podaci[5], out Status);

            string[] idOfTheComponents = podaci[6].Split(",");

            List<Komponenta> listaKomponenataAdd = new List<Komponenta>(); ;

            for (int i = 0; i < idOfTheComponents.Length; i++)
            {
                Artikal komponentaLoad = listaArtikla.Where(x => x.Sifra == Convert.ToInt32(idOfTheComponents[i])).FirstOrDefault();

                Komponenta komponentaConvert = komponentaLoad as Komponenta;

                if (komponentaConvert != null)
                {
                    listaKomponenataAdd.Add(komponentaConvert);
                }
                else
                {
                    Console.WriteLine("Error while adding component!");
                }
            }

            ListaKomponenata = listaKomponenataAdd;
        }

        public List<Komponenta> ListaKomponenata;

        public override string Save()
        {
            string data = string.Empty;
            string id = string.Empty;

            for (int i = 0; i < ListaKomponenata.Count; i++)
            {
                id += ListaKomponenata[i].Sifra + ",";
            }

            if (id.EndsWith(","))
            {
                id = id.Remove(id.Length - 1, 1);
            }

            data = Sifra + ";" + Naziv + ";" + Cena + ";" + Kolicina + ";" + Opis + ";" + Status.ToString() + ";" + id;

            return data;
        }
    }
}

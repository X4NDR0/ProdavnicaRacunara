using Prodavnica_Racunara.Utils;

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
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            Opis = podaci[2];
        }

        public int Sifra;
        public string Naziv;
        public string Opis;

        public string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Opis;
            return data;
        }
    }
}

namespace Prodavnica_Racunara.Models
{
    class StavkaRacuna : Racun
    {
        public int ID;
        public KupljeniArtikal ProdatArtikal;
        public double Cena;
        public int Kolicina;
    }
}

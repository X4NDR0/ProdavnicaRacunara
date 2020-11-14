namespace Prodavnica_Racunara.Models
{
    /// <summary>
    /// Representing class category
    /// </summary>
    public class Kategorija
    {
        /// <summary>
        /// Empty class constructor
        /// </summary>
        public Kategorija()
        {

        }

        /// <summary>
        /// Class constructor with parametars
        /// </summary>
        /// <param name="data"></param>
        public Kategorija(string data)
        {
            string[] podaci = data.Split(';');
            int.TryParse(podaci[0], out Sifra);
            Naziv = podaci[1];
            Opis = podaci[2];
        }

        /// <summary>
        /// Representing ID of the category
        /// </summary>
        public int Sifra;

        /// <summary>
        /// Representing name of the category
        /// </summary>
        public string Naziv;

        /// <summary>
        /// Representing category description
        /// </summary>
        public string Opis;

        /// <summary>
        /// Representing method which save data in csv file
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            string data = Sifra + ";" + Naziv + ";" + Opis;
            return data;
        }
    }
}

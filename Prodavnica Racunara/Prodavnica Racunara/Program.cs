using Prodavnica_Racunara.Services;

namespace Prodavnica_Racunara
{
    class Program
    {
        static void Main(string[] args)
        {
            ProdavnicaRacunaraService prs = new ProdavnicaRacunaraService();
            prs.Login();
        }
    }
}

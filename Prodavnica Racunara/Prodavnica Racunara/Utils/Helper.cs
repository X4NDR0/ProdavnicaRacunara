using System;
using System.Collections.Generic;
using System.Text;

namespace Prodavnica_Racunara.Utils
{
    public class Helper
    {
        public static int IDArtikal;
        
        public int ProveraInt()
        {
            int broj;
            while (int.TryParse(Console.ReadLine(), out broj) == false)
            {
                Console.Write("Pogresan unos,pokusajte ponovo:");
            }
            return broj;
        }

        public double ProveraDouble()
        {
            double broj;
            while (double.TryParse(Console.ReadLine(), out broj) == false)
            {
                Console.Write("Pogresan unos,pokusajte ponovo:");
            }
            return broj;
        }
    }
}

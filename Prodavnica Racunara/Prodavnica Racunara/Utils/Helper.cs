using System;
using System.Collections.Generic;
using System.Text;

namespace Prodavnica_Racunara.Utils
{
    /// <summary>
    /// Representing class
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Representing ID of the artikal
        /// </summary>
        public static int IDArtikal;

        /// <summary>
        /// Representing id of the "racun"
        /// </summary>
        public static int IDRacuna;

        /// <summary>
        /// Representing ID of the "stavke racuna"
        /// </summary>
        public static int IDStavkeRacuna;

        /// <summary>
        /// Representing method which check if input is int
        /// </summary>
        /// <returns></returns>
        public static int ProveraInt()
        {
            int broj;
            while (int.TryParse(Console.ReadLine(), out broj) == false)
            {
                Console.Write("Wrong input try again:");
            }
            return broj;
        }

        /// <summary>
        /// Representing method which check if input is double
        /// </summary>
        /// <returns></returns>
        public static double ProveraDouble()
        {
            double broj;
            while (double.TryParse(Console.ReadLine(), out broj) == false)
            {
                Console.Write("Wrong input try again:");
            }
            return broj;
        }

        /// <summary>
        /// Representing method which check if input is string
        /// </summary>
        /// <returns></returns>
        public static string ProveraStringa()
        {
            string text = string.Empty;
            while (text.Equals(""))
            {
                Console.Write("Wrong input try again:");
                text = Console.ReadLine();
            }
            return text;
        }

        /// <summary>
        /// Representing method which check if input is date time
        /// </summary>
        /// <returns></returns>
        public static DateTime ProveraVremena()
        {
            DateTime vreme;
            while (DateTime.TryParse(Console.ReadLine(),out vreme))
            {
                Console.Write("Wrong input try again:");
            }
            return vreme;
        }
    }
}

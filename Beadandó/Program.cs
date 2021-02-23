/*
1. Készíts programot, ami kiírja a mindenkori aktuális dátumot úgy, hogy a hónap római számmmal
íródik! (pl. 2015. II. 27)
2. Készíts void függvényt, ami a sík egy derékszögű koordinátarendszerben adott pontját tükrözi a függőleges tengely mentén!
3. Készíts függvényt, ami a paraméterében megadott iterációszámmal közelítőleg visszaadja a PI értékét! Használjuk a Gregory-Leibniz sorozatot:
? = (4/1) - (4/3) + (4/5) - (4/7) + (4/9) - (4/11) + (4/13) - (4/15) + ...
4. Tároljuk nullázható változóban egy hallgató vizsgaeredményét egy tárgyból: 1, 2, 3, 4, 5, null(nincs kitöltve az érték, vagy törölve)!
Készíts programot, ami képes ezt változtatni, és kiírni az aktuális állapotot!
A változtatás és a kiírás menüpontokból választható legyen!
 */

using System;
using System.Collections.Generic;

namespace Beadandó
{
    internal enum RomaiSzamok
    {
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        V = 5,
        VI = 6,
        VII = 7,
        VIII = 8,
        IX = 9,
        X = 10,
        XI = 11,
        XII = 12
    }

    internal class Program
    {
        /*
         * Amikor túl lusta vagyok 12 értékpárt beírni enum-ba, ezért inkább eltöltök 10x annyi időt,
         * hogy írjak egy algoritmust, amely ezt automatizálja, és végül 5 kivételt mégis megadok...
         */
        private static readonly Dictionary<int, string> ArabRomai = new Dictionary<int, string>
        {
            {10,"X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };

        private static string ArabRomaiKoverzio(int arabSzam)
        { 
            var romaiSzam = string.Empty;
	
            foreach (var arabErtek in ArabRomai.Keys)
            {
                while (arabSzam >= arabErtek)
                {
                    romaiSzam += ArabRomai[arabErtek];
                    arabSzam -= arabErtek;
                }
            }
            return romaiSzam;
        }

        
        
        public static void Main(string[] args)
        {
            DateTime date = DateTime.Today;
            /*
            for (int i = 1; i <= 12; i++)
            {
                romaiSzamok = (RomaiSzamok) Enum.Parse(typeof(RomaiSzamok), i.ToString());
                Console.WriteLine((int) romaiSzamok + " " + romaiSzamok);
            }

            for (int i = 1; i <= 12; i++)
            {
                Console.WriteLine(i + " " + ArabRomaiKoverzio(i));
            }
            */
            Console.WriteLine("Mai datum: " + date.Year + "." + ArabRomaiKoverzio(date.Month) + "." + date.Day + ".");
            Console.WriteLine("Mai datum: " + date.Year + "." + Enum.Parse(typeof(RomaiSzamok), date.Month.ToString()) + "." + date.Day + ".");
            
        }
    }
}
﻿/*
 * Fábián Gábor
 * CXNU8T
 * https://github.com/FabianGabor/Programozasi-kornyezetek-03-Beadando
 */

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
using System.Linq;

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

    internal class Pont
    {
        public Pont(double x = default, double y = default)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        // Készíts void függvényt, ami a sík egy derékszögű koordinátarendszerben adott pontját tükrözi a függőleges tengely mentén!
        public static void KoordinataTukrozFuggolegesTengely(in Pont p, out Pont pont)
        {
            pont = p;
            pont.X *= -1;
        }
    }

    /*
    3. Készíts függvényt, ami a paraméterében megadott iterációszámmal közelítőleg visszaadja a PI értékét! Használjuk a Gregory-Leibniz sorozatot:
    ? = (4/1) - (4/3) + (4/5) - (4/7) + (4/9) - (4/11) + (4/13) - (4/15) + ...
    */
    internal class Pi
    {
        private double Value { get; set; }

        public double Calculate(in int n)
        {
            Value = 0;
            for (var i = 0; i < n; i++)
            {
                // Value += Math.Pow(-1, i) * (4.0 / (2 * i + 1)); // 2860 ms + 2.52% get 162ms + 1.77% set 114 ms
                // value += Math.Pow(-1, i) * (4.0 / (2 * i + 1)); // 2860 ms
                
                if (i % 2 == 0)
                    Value += 4.0 / (2 * i + 1);
                else
                    Value -= 4.0 / (2 * i + 1); // 362 ms
            }

            return Value;
        }
    }
    
    /*
    4. Tároljuk nullázható változóban egy hallgató vizsgaeredményét egy tárgyból: 1, 2, 3, 4, 5, null(nincs kitöltve az érték, vagy törölve)!
    Készíts programot, ami képes ezt változtatni, és kiírni az aktuális állapotot!
    A változtatás és a kiírás menüpontokból választható legyen!
    */
    [Serializable]
    public class HibasVizsgaeredmeny : Exception
    {
        public HibasVizsgaeredmeny()
        {
        }

        public HibasVizsgaeredmeny(string message)
            : base(message)
        {
        }
    }

    internal class Hallgato
    {
        private int? _vizsgaeredmeny;

        public int? Vizsgaeredmeny
        {
            get => _vizsgaeredmeny;
            set {
                if (value > 0 && value < 6 || value == null)
                    _vizsgaeredmeny = value;
                else
                {
                    try
                    {
                        throw new HibasVizsgaeredmeny("Hibás vizsgaeredmény!");
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void Valtoztat()
        {
            var loop = false;
            do
            {
                if (loop) Console.WriteLine("Hibás jegy!");
                Console.Write("Új jegy (1-5, null): ");
                var success = int.TryParse(Console.ReadLine(), out var v);
                Vizsgaeredmeny = success ? v : (int?) null;
                loop = Vizsgaeredmeny < 1 || Vizsgaeredmeny > 5;
            } while (loop);

        }

        public void Kiir()
        {
            Console.Write("Hallgató vizsgaeredménye: ");
            if (_vizsgaeredmeny != null)
            {
                Console.WriteLine(_vizsgaeredmeny);
            }
            else
            {
                Console.WriteLine("null");
            }
        }

        public override string ToString()
        {
            return _vizsgaeredmeny != null ? _vizsgaeredmeny.Value.ToString() : "null";
        }
    }

    internal class Menu
    {
        private enum Opciok
        {
            Változtat = 1,
            Kiír = 2,
            Kilép = 3
        }

        private int Valasz { get; set; }

        public override string ToString()
        {
            return ((Opciok[]) Enum.GetValues(typeof(Opciok))).Aggregate("", (current, opcio) => current + opcio.GetHashCode() + ". " + opcio + "\n");
        }

        public void Show(Hallgato hallgato)
        {
            do
            {
                Console.WriteLine(ToString());
                Console.Write("Válasz: ");
                
                var valasz = Valasz;
                var success = int.TryParse(Console.ReadLine(), out valasz);
                Valasz = success ? valasz : 0;
                
                switch (Valasz)
                {
                    case 1:
                        hallgato.Valtoztat();
                        //Console.WriteLine(Valasz + ". " + (Opciok) Valasz);
                        break;
                    case 2:
                        hallgato.Kiir();
                        //Console.WriteLine(Valasz + ". " + (Opciok) Valasz);
                        break;
                    case 3:
                        //Console.WriteLine(Valasz + ". " + (Opciok) Valasz);
                        return;
                    default:
                        Console.WriteLine("Hibás opció!");
                        break;
                }
            } while (true);  // (Valasz != 3); // ott return van
        }
    }

    internal static class Program
    {
        /*
         * Amikor túl lusta vagyok 12 értékpárt beírni enum-ba, ezért inkább eltöltök 10x annyi időt,
         * hogy írjak egy algoritmust, amely ezt automatizálja, és végül 5 kivételt mégis megadok...
         */
        private static readonly Dictionary<int, string> ArabRomai = new Dictionary<int, string>
        {
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };

        private static string ArabRomaiKoverzio(int arabSzam)
        {
            var romaiSzam = string.Empty;

            foreach (var arabErtek in ArabRomai.Keys)
                while (arabSzam >= arabErtek)
                {
                    romaiSzam += ArabRomai[arabErtek];
                    arabSzam -= arabErtek;
                }

            return romaiSzam;
        }

        public static void Main()
        {
            var date = DateTime.Today;
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
            Console.WriteLine("Mai datum: " + date.Year + "." + Enum.Parse(typeof(RomaiSzamok), date.Month.ToString()) +
                              "." + date.Day + "." + "\n");

            var p = new Pont(1, 1);
            Console.WriteLine("(" + p.X + "," + p.Y + ")");
            Pont.KoordinataTukrozFuggolegesTengely(in p, out p);
            Console.WriteLine("(" + p.X + "," + p.Y + ")");
            
            var pi = new Pi();
            Console.WriteLine(pi.Calculate(1000000) + "\n");


            var hallgato = new Hallgato {Vizsgaeredmeny = null};
            var menu = new Menu();
            menu.Show(hallgato);

        }
    }
}
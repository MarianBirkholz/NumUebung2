using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumUebung2
{
    class Program
    {

        static List<double> stuetzStellen = new List<double>();
        static List<double> stuetzWerte = new List<double>();
        static List<List<double>> stuetzLagra = new List<List<double>>();
        static List<double> lagraPolynome = new List<double>();
        static List<List<double>> newtonBasis = new List<List<double>>();
        static List<double> newtonPolynome = new List<double>();
        static List<double> hermit = new List<double>();
        static Dictionary<int, double> diviDiffDic = new Dictionary<int, double>();

        static void Main(string[] args)
        {
            bool startOver = true;
            char nochmal = 'n';
            while (startOver)
            {
                double eingabeWert;
                bool eingabe = true;
                int c = 1;

                Console.WriteLine("Bitte Stützstellen (y) und Stützwerte (x) eingeben!");
                Console.WriteLine("fertig? dann [esc] druecken");
                while (eingabe)
                {
                    Console.WriteLine();
                    while (!ReadChecker(out eingabeWert, "x" + c))
                    {
                        if (stuetzStellen.Contains(eingabeWert))
                        {
                            //run hermite
                        }
                        stuetzStellen.Add(eingabeWert);
                        newtonPolynome.Add(0);
                        hermit.Add(0);
                    }
                    while (!ReadChecker(out eingabeWert, "y" + c))
                    {
                        stuetzWerte.Add(eingabeWert);
                    }
                    ConsoleKeyInfo cki;
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        eingabe = false;
                    }
                    c++;
                }

                
                Console.WriteLine("Nochmal? (j/n)");
                try
                {
                    nochmal = char.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Da lief etwas schief bitte nocheinmal versuchen. Mit Ja(j) oder Nein(n)");
                }
                if (nochmal =='n')
                {
                    Console.WriteLine("Das wars tschau. SELBST ZERSTOERUNG AKTIVIERT");
                    System.Threading.Thread.Sleep(10000);
                    System.Environment.Exit(0);

                }
            }
        }

        //Berechnung Fakulteat
        public static double Fakultaet(double z)
        {
            double result = 1;
            for(int i = 1; i <= z; i++)
            {
                result *= i;
            }
            return result;
        }

        //Lagrange
        public static void lagrange()
        {
            for(int i = 0; i < stuetzStellen.Count; i++)
            {
                stuetzLagra.Add(rechnung(i));
            }
        }

        public static List<double> rechnung(int s)
        {
            List<double> polynom = new List<double>();
            double result = 1;

            for (int i = 0; i < stuetzStellen.Count; i++)
            {
                if(i == s)
                {
                    result = result * (stuetzStellen[s] - stuetzStellen[i]);
                }
                if(polynom.Count == 0)
                {
                    polynom.Add(stuetzStellen[i] * -1);
                    polynom.Add(1);
                }
                else
                {
                    polynom.Insert(0, 0);
                    for (int j = 1; j< polynom.Count; j++)
                    {
                        polynom[j - 1] = (polynom[j] * (stuetzStellen[i] * -1) + polynom[j - 1]);
                    }
                }
            }
            for (int i = 0; i < polynom.Count; i++)
            {
                polynom[i] /= result;
            }
            AusgabeLagrange(polynom, s);
            return polynom;
        }

        public static void LagrangeRest()
        {
            string ausgabePolynom = "";
            List<double> polynom = new List<double>();

            for (int i = 0;i<stuetzLagra.Count; i++)
            {
                for(int j = 0; j < stuetzLagra[i].Count; j++)
                {
                    stuetzLagra[i][j] *= stuetzWerte[i];
                }
                if (i == 0)
                {
                    lagraPolynome = stuetzLagra[i];
                }
                else
                {
                    lagraPolynome = lagraPolynome.Zip(stuetzLagra[i], (a, b) => (a + b)).ToList<double>();
                }
            }
            ausgabePolynom += "p(x) = ";
            for(int i = lagraPolynome.Count - 1; i >= 0; i--)
            {
                ausgabePolynom += (lagraPolynome[i] >= 0 ? " +" : " ") + lagraPolynome[i] + (i != 0 ? "x^" + i : "");
            }
            Console.WriteLine();
            Console.WriteLine("************ Lagrange Polynom Ausmultipliziert: ************");
            Console.WriteLine(ausgabePolynom);
        }

        //Ausgabe

        public static void AusgabeLagrange(List<double> lagra, int position)
        {
            string ausgabePolynom = "|" + (position + 1) + "(x) =";

            for (int i = lagra.Count - 1; i >= 0; i--)
            {
                ausgabePolynom += (lagra[i] >= 0 ? "+" : " ") + lagra[i] + (i != 0 ? "x^" + i : "");
            }

            Console.WriteLine(ausgabePolynom);

        }

        //Eingabe + Fehler pruefung

        public static bool ReadChecker(out double wert, string abfrage)
        {
            Console.Write(abfrage + ": ");

            try
            {
                wert = double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Geben Sie bitte eine Zahl ein.");
                wert = 0;
                return false;
            }
            return true;
        }
    }
}

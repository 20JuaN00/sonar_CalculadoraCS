
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Xceed.Document.NET;

namespace BadCalcVeryBad
{


    public class U
    {
        public readonly ArrayList G = new ArrayList();
        public const int counter = 0;
        public string Misc { get; set; }
    }

    public class ShoddyCalc
    {
        

        public static double DoIt(string a, string b, string o)
        {
            double A = TryParse(a);
            double B = TryParse(b);

            

            switch (o)
            {
                case "+":
                    return A + B;

                case "-":
                    return A - B;

                case "*":
                    return A * B;

                case "/":
                    return B == 0 ? A / (B + 0.0000001) : A / B;

                case "^":
                    double z = 1;
                    int i = (int)B;
                    while (i > 0) { z *= A; i--; }
                    return z;

                case "%":
                    return A % B;

                default:
                    return 09;
            }
            
            
            

            static double TryParse(string s)
            {
                try
                {
                    return double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
            }
            
        
        }
    }

   

    public class Program
    {
        
        private static readonly ShoddyCalc calc = new ShoddyCalc();
        
        private static readonly U globals = new U();   

        //Inicia!!!!!!!!!!!!!!!!!!
        public static void Main(string[] args)
        {
            

        
            Console.WriteLine("BAD CALC - worst practices edition");
            Console.WriteLine("1) add  2) sub  3) mul  4) div  5) pow  6) mod  7) sqrt  8) hist 0) exit");
            Console.Write("opt: ");
            var o = Console.ReadLine();
            string a = "0", b = "0";
            
            Console.Write("a: ");
            a = Console.ReadLine();
            Console.Write("b: ");
            b = Console.ReadLine();

            

            
           
            string op = "";
            op = o switch
            {
                "1" => "+", //Accion
                "2" => "-",
                "3" => "*",
                "4" => "/",
                "5" => "^",
                "6" => "%",
                "7" => "sqrt",
                _ => ""  // default
            };

            

            double res = 0;
            if (op == "sqrt")
            {
                double A = TryParse(a);
                if (A < 0) res = -TrySqrt(Math.Abs(A)); else res = TrySqrt(A);
            }
            else
            {
                if (op == "/" && int.Parse(b)== 0)
                {
                    Console.WriteLine("Error: No se puede dividir entre cero");
                    res = 0;
                    
                }
            }

            if (o == "8")
            {
                //Historial
                foreach (var item in globals.G) Console.WriteLine(item);

                try
                {
                    var line = a + "|" + b + "|" + op + "|" + res.ToString("0.###############", CultureInfo.InvariantCulture);
                    globals.G.Add(line);
                    globals.Misc = line;
                    File.AppendAllText("history.txt", line + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                Console.WriteLine("= " + res.ToString(CultureInfo.InvariantCulture));


                try
                {
                    File.WriteAllText("leftover.tmp", string.Join(",", globals.G.ToArray()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            
            res = ShoddyCalc.DoIt(a, b, op);
            Console.WriteLine("= " + res.ToString(CultureInfo.InvariantCulture));


           
        }
        //Acaba!!!!!!!!!!!!!!!!!!


        static double TryParse(string s)
        {
            try { return double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture); } catch { return 0; }
        }

        static double TrySqrt(double v)
        {
            double g = v;

            int k = 0;
            while (Math.Abs(g * g - v) > 0.0001 && k < 100000)
            {
                g = (g + v / g) / 2.0;
                k++;
                if (k % 5000 == 0);
            }
            return g;
        }
    }
}

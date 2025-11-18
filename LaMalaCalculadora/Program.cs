
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


        public class DatosGlobales
        {
            public readonly ArrayList Historial = new ArrayList();
            public const int contador = 0;
            public string Misc { get; set; }
        }

        public static class Calculadora
        {
        

            public static double Calcular(string a, string b, string operador)
            {
                double NumA = ConvertirTexto(a);
                double NumB = ConvertirTexto(b);

            

                switch (operador)
                {
                    case "+":
                        return NumA + NumB;

                    case "-":
                        return NumA + NumB;

                case "*":
                        return NumA + NumB;

                case "/":
                        if (Math.Abs(NumB) < 0.0000001)  
                        {
                            throw new DivideByZeroException("No se puede dividir entre cero");
                        }
                        return NumA + NumB;



                case "^":
                        double z = 1;
                        int i = (int)NumB;
                        while (i > 0) { z *= NumA; i--; }
                        return z;

                    case "%":
                        return NumA % NumB;

                    default:
                        Console.WriteLine("Numero equivocado intente de nuevo");
                        return 0;
                }
            
            
            

                static double ConvertirTexto(string texto)
                {
                    try
                    {
                        return double.Parse(texto.Replace(',', '.'), CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return 0;
                    }
                }
            
        
            }
        }

   

        public static class Program
        {
        
            private static readonly DatosGlobales globals = new DatosGlobales();   

            //Inicia!!!!!!!!!!!!!!!!!!
            public static void Main(string[] args)
            {
                try
                {
                    string operacion = "";
                    do
                    {
                        Console.WriteLine("BAD CALC - worst practices edition");
                        Console.WriteLine("1) add  2) sub  3) mul  4) div  5) pow  6) mod  7) sqrt  8) hist 0) exit");
                        Console.Write("opt: ");
                        var o = Console.ReadLine();
                        string a = "0", b = "0";
                        double res = 0;

                        if (int.Parse(o) < 0 || int.Parse(o) > 8)
                        {
                            Console.WriteLine("Opcion invalida, intente de nuevo");
                            continue;
                        }

                        operacion = o switch
                        {
                            "1" => "+", //Accion
                            "2" => "-",
                            "3" => "*",
                            "4" => "/",
                            "5" => "^",
                            "6" => "%",
                            "7" => "sqrt",
                            "8" => "hist",
                            "0" => "exit",
                            _ => ""  // default
                        };


                        if (operacion == "exit")
                        {
                            Console.WriteLine("Saliendo...");
                            return;
                        }

                        else if (operacion == "hist")
                        {
                            //Historial
                            Console.WriteLine("\n=== HISTORY ===");
                            foreach (var item in globals.Misc) Console.WriteLine(item);
                        continue;



                        }

                        Console.Write("a: ");
                        a = Console.ReadLine();

                        if (operacion != "sqrt" )
                        {
                        
                            Console.Write("b: ");
                            b = Console.ReadLine();
                        }

                         if (operacion == "sqrt")
                         {
                        
                            double A = TryParse(a);
                            if (A < 0) res = TrySqrt(Math.Abs(A)); else res = TrySqrt(A);

                        

                         }


                    

                        else
                        {

                            

                            res = Calculadora.Calcular(a, b, operacion);
                        

                        }
                        Console.WriteLine("= " + res.ToString(CultureInfo.InvariantCulture));

                        //Guardar en historial
                        try
                        {
                            var line = a + "|" + b + "|" + operacion + "|" + res.ToString("0.###############", CultureInfo.InvariantCulture);
                            globals.Historial.Add(line);
                            globals.Misc = line;
                            File.AppendAllText("history.txt", line + Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }



                    } while (operacion != "exit");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ingrese un dato correcto ");    
                }
            
            

        
            

           
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
                
                }
                return g;
            }
        }
    }

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Diccionarios bidireccionales iniciales con las 21 palabras proporcionadas
        Dictionary<string, string> inglesAEspanol = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"time", "tiempo"}, {"person", "persona"}, {"year", "año"}, {"way", "camino"},
            {"day", "día"}, {"thing", "cosa"}, {"man", "hombre"}, {"world", "mundo"},
            {"life", "vida"}, {"hand", "mano"}, {"part", "parte"}, {"child", "niño"},
            {"eye", "ojo"}, {"woman", "mujer"}, {"place", "lugar"}, {"work", "trabajo"},
            {"week", "semana"}, {"case", "caso"}, {"point", "punto"}, {"government", "gobierno"},
            {"company", "empresa"}
        };

        Dictionary<string, string> espanolAIngles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"tiempo", "time"}, {"persona", "person"}, {"año", "year"}, {"camino", "way"},
            {"día", "day"}, {"cosa", "thing"}, {"hombre", "man"}, {"mundo", "world"},
            {"vida", "life"}, {"mano", "hand"}, {"parte", "part"}, {"niño", "child"},
            {"ojo", "eye"}, {"mujer", "woman"}, {"lugar", "place"}, {"trabajo", "work"},
            {"semana", "week"}, {"caso", "case"}, {"punto", "point"}, {"gobierno", "government"},
            {"empresa", "company"}
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n=== DUOLINGUE UEA ===");
            Console.WriteLine("=======================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Ingresar más palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("\nIngrese la frase a traducir: ");
                    string frase = Console.ReadLine();
                    string[] palabras = frase.Split(' ');

                    // Determinar el idioma de entrada
                    bool esEspanol = ContarPalabrasConocidas(palabras, espanolAIngles) > 
                                   ContarPalabrasConocidas(palabras, inglesAEspanol);

                    string resultado = "";
                    Dictionary<string, string> diccionario = esEspanol ? espanolAIngles : inglesAEspanol;

                    foreach (string palabra in palabras)
                    {
                        string palabraLimpia = palabra.Trim(',', '.', '!', '?');
                        if (diccionario.ContainsKey(palabraLimpia))
                        {
                            resultado += diccionario[palabraLimpia] + " ";
                        }
                        else
                        {
                            resultado += palabra + " ";
                        }
                    }

                    Console.WriteLine($"\nSu frase traducida es: {resultado.Trim()}");
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Write("\nIngrese la palabra en inglés: ");
                    string palabraIngles = Console.ReadLine().ToLower();
                    Console.Write("Ingrese su traducción en español: ");
                    string palabraEspanol = Console.ReadLine().ToLower();

                    if (!string.IsNullOrEmpty(palabraIngles) && !string.IsNullOrEmpty(palabraEspanol))
                    {
                        inglesAEspanol[palabraIngles] = palabraEspanol;
                        espanolAIngles[palabraEspanol] = palabraIngles;
                        Console.WriteLine("\n¡Palabras agregadas exitosamente al diccionario!");
                    }
                    else
                    {
                        Console.WriteLine("\nError: Las palabras no pueden estar vacías.");
                    }

                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                    break;

                case "0":
                    Console.WriteLine("\n¡Gracias por usar Duolingue UEA! Hasta pronto.");
                    Console.ReadLine();
                    return;

                default:
                    Console.WriteLine("\nOpción no válida. Por favor, seleccione 0, 1 o 2.");
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static int ContarPalabrasConocidas(string[] palabras, Dictionary<string, string> diccionario)
    {
        int contador = 0;
        foreach (string palabra in palabras)
        {
            string palabraLimpia = palabra.Trim(',', '.', '!', '?');
            if (diccionario.ContainsKey(palabraLimpia))
            {
                contador++;
            }
        }
        return contador;
    }
}
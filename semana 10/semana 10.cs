using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanVCOVID
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generar el conjunto total de 500 ciudadanos
            HashSet<string> ciudadanos = GenerarCiudadanos(500);
            Console.WriteLine($"Total de ciudadanos registrados: {ciudadanos.Count}");

            // Crear conjuntos de ciudadanos vacunados con Pfizer y AstraZeneca
            HashSet<string> vacunadosPfizer = GenerarVacunados(ciudadanos, 75, "Pfizer");
            HashSet<string> vacunadosAstraZeneca = GenerarVacunados(ciudadanos, 75, "AstraZeneca");

            // Determinar ciudadanos con ambas vacunas usando intersección
            HashSet<string> vacunadosAmbas = new HashSet<string>(vacunadosPfizer);
            vacunadosAmbas.IntersectWith(vacunadosAstraZeneca);

            // Listado 1: Ciudadanos no vacunados (diferencia con los vacunados)
            HashSet<string> noVacunados = new HashSet<string>(ciudadanos);
            noVacunados.ExceptWith(vacunadosPfizer);
            noVacunados.ExceptWith(vacunadosAstraZeneca);

            // Listado 2: Ciudadanos solo con Pfizer (diferencia con AstraZeneca)
            HashSet<string> soloPfizer = new HashSet<string>(vacunadosPfizer);
            soloPfizer.ExceptWith(vacunadosAstraZeneca);

            // Listado 3: Ciudadanos solo con AstraZeneca (diferencia con Pfizer)
            HashSet<string> soloAstraZeneca = new HashSet<string>(vacunadosAstraZeneca);
            soloAstraZeneca.ExceptWith(vacunadosPfizer);

            // Listado 4: Ciudadanos con ambas vacunas
            HashSet<string> conDosVacunas = vacunadosAmbas;

            // Mostrar resultados en consola
            Console.WriteLine("\n1. Ciudadanos sin vacunar: " + noVacunados.Count);
            MostrarConjunto(noVacunados, 5); // Mostrar solo 5 para no llenar la pantalla

            Console.WriteLine("\n2. Ciudadanos con dos vacunas: " + conDosVacunas.Count);
            MostrarConjunto(conDosVacunas, 5);

            Console.WriteLine("\n3. Ciudadanos solo con Pfizer: " + soloPfizer.Count);
            MostrarConjunto(soloPfizer, 5);

            Console.WriteLine("\n4. Ciudadanos solo con AstraZeneca: " + soloAstraZeneca.Count);
            MostrarConjunto(soloAstraZeneca, 5);
        }

        // Método para generar el conjunto de 500 ciudadanos ficticios
        static HashSet<string> GenerarCiudadanos(int cantidad)
        {
            HashSet<string> ciudadanos = new HashSet<string>();
            for (int i = 1; i <= cantidad; i++)
            {
                ciudadanos.Add($"Ciudadano_{i}"); // Nombres como Ciudadano_1, Ciudadano_2, etc.
            }
            return ciudadanos;
        }

        // Método para crear subconjuntos de ciudadanos vacunados aleatoriamente
        static HashSet<string> GenerarVacunados(HashSet<string> ciudadanos, int cantidad, string tipoVacuna)
        {
            Random random = new Random();
            HashSet<string> vacunados = new HashSet<string>();
            List<string> listaCiudadanos = ciudadanos.ToList(); // Convertir a lista para selección aleatoria

            while (vacunados.Count < cantidad)
            {
                int indice = random.Next(0, listaCiudadanos.Count); // Elegir un índice al azar
                vacunados.Add(listaCiudadanos[indice]);
            }
            return vacunados;
        }

        // Método para mostrar un subconjunto limitado de elementos
        static void MostrarConjunto(HashSet<string> conjunto, int limite)
        {
            foreach (var item in conjunto.Take(limite)) // Tomar solo los primeros 'limite' elementos
            {
                Console.WriteLine(item);
            }
            if (conjunto.Count > limite) Console.WriteLine("..."); // Indicar que hay más
        }
    }
}
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Crear el catálogo de revistas inicial
        List<string> catalogoRevistas = new List<string>
        {
            "National Geographic",
            "Time",
            "Vogue",
            "Scientific American",
            "The Economist",
            "Rolling Stone",
            "Forbes",
            "Nature",
            "Wired",
            "People"
        };

        // Menú interactivo
        while (true)
        {
            Console.WriteLine("\n=== Menú de Búsqueda de Revistas ===");
            Console.WriteLine("1. Buscar un título");
            Console.WriteLine("2. Mostrar catálogo completo");
            Console.WriteLine("3. Agregar un título");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            // Manejar posible null en la entrada
            string? opcion = Console.ReadLine();
            if (string.IsNullOrEmpty(opcion))
            {
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
                continue;
            }

            if (opcion == "0")
            {
                Console.WriteLine("Saliendo del programa...");
                break;
            }
            else if (opcion == "1")
            {
                Console.Write("Ingrese el título a buscar: ");
                string? tituloBuscado = Console.ReadLine();
                if (string.IsNullOrEmpty(tituloBuscado))
                {
                    Console.WriteLine("El título no puede estar vacío.");
                    continue;
                }

                // Llamada al método de búsqueda
                bool encontrado = BuscarTituloIterativo(catalogoRevistas, tituloBuscado);
                Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
            }
            else if (opcion == "2")
            {
                Console.WriteLine("\nCatálogo de revistas:");
                foreach (string revista in catalogoRevistas)
                {
                    Console.WriteLine($"- {revista}");
                }
            }
            else if (opcion == "3")
            {
                Console.Write("Ingrese el título de la revista a agregar: ");
                string? nuevoTitulo = Console.ReadLine();

                // Verificar si el título es vacío o nulo
                if (string.IsNullOrWhiteSpace(nuevoTitulo))
                {
                    Console.WriteLine("No se puede agregar un título vacío.");
                    continue;
                }

                // Verificar si el título ya existe
                if (BuscarTituloIterativo(catalogoRevistas, nuevoTitulo))
                {
                    Console.WriteLine("El título ya existe en el catálogo.");
                }
                else
                {
                    catalogoRevistas.Add(nuevoTitulo);
                    Console.WriteLine($"Título '{nuevoTitulo}' agregado al catálogo.");
                }
            }
            else
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
            }
        }
    }

    // Método de búsqueda iterativa ajustado para manejar strings
    static bool BuscarTituloIterativo(List<string> catalogo, string titulo)
    {
        foreach (string revista in catalogo)
        {
            if (revista.Equals(titulo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
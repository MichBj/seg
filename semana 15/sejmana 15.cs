using System;

public class Nodo
{
    public string Dato { get; set; } = null!; // Suppress warning
    public Nodo? Izquierdo { get; set; }
    public Nodo? Derecho { get; set; }

    public Nodo(string dato)
    {
        Dato = dato;
        Izquierdo = null;
        Derecho = null;
    }
}

public class ArbolBinario
{
    private Nodo? raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    public void Insertar(string dato)
    {
        if (raiz == null)
        {
            raiz = new Nodo(dato);
        }
        else
        {
            InsertarRecursivo(raiz, dato);
        }
    }

    private void InsertarRecursivo(Nodo? nodo, string dato)
    {
        if (nodo == null) return;

        if (string.Compare(dato, nodo.Dato) < 0)
        {
            if (nodo.Izquierdo == null)
            {
                nodo.Izquierdo = new Nodo(dato);
            }
            else
            {
                InsertarRecursivo(nodo.Izquierdo, dato);
            }
        }
        else
        {
            if (nodo.Derecho == null)
            {
                nodo.Derecho = new Nodo(dato);
            }
            else
            {
                InsertarRecursivo(nodo.Derecho, dato);
            }
        }
    }

    public void PreOrden()
    {
        Console.WriteLine("Recorrido Pre-Orden:");
        PreOrdenRecursivo(raiz);
        Console.WriteLine();
    }

    private void PreOrdenRecursivo(Nodo? nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.Dato + " ");
            PreOrdenRecursivo(nodo.Izquierdo);
            PreOrdenRecursivo(nodo.Derecho);
        }
    }

    public void InOrden()
    {
        Console.WriteLine("Recorrido In-Orden:");
        InOrdenRecursivo(raiz);
        Console.WriteLine();
    }

    private void InOrdenRecursivo(Nodo? nodo)
    {
        if (nodo != null)
        {
            InOrdenRecursivo(nodo.Izquierdo);
            Console.Write(nodo.Dato + " ");
            InOrdenRecursivo(nodo.Derecho);
        }
    }

    public void PostOrden()
    {
        Console.WriteLine("Recorrido Post-Orden:");
        PostOrdenRecursivo(raiz);
        Console.WriteLine();
    }

    private void PostOrdenRecursivo(Nodo? nodo)
    {
        if (nodo != null)
        {
            PostOrdenRecursivo(nodo.Izquierdo);
            PostOrdenRecursivo(nodo.Derecho);
            Console.Write(nodo.Dato + " ");
        }
    }

    public bool Buscar(string dato)
    {
        return BuscarRecursivo(raiz, dato);
    }

    private bool BuscarRecursivo(Nodo? nodo, string dato)
    {
        if (nodo == null)
        {
            return false;
        }
        if (nodo.Dato == dato)
        {
            return true;
        }
        if (string.Compare(dato, nodo.Dato) < 0)
        {
            return BuscarRecursivo(nodo.Izquierdo, dato);
        }
        return BuscarRecursivo(nodo.Derecho, dato);
    }

    public int CalcularAltura()
    {
        return CalcularAlturaRecursivo(raiz);
    }

    private int CalcularAlturaRecursivo(Nodo? nodo)
    {
        if (nodo == null)
        {
            return 0;
        }
        int alturaIzquierda = CalcularAlturaRecursivo(nodo.Izquierdo);
        int alturaDerecha = CalcularAlturaRecursivo(nodo.Derecho);
        return Math.Max(alturaIzquierda, alturaDerecha) + 1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolBinario arbol = new ArbolBinario();
        int opcion;

        do
        {
            Console.WriteLine("\n=== Menú Árbol Binario (Nombres) ===");
            Console.WriteLine("1. Insertar un nombre");
            Console.WriteLine("2. Recorrido Pre-Orden");
            Console.WriteLine("3. Recorrido In-Orden");
            Console.WriteLine("4. Recorrido Post-Orden");
            Console.WriteLine("5. Buscar un nombre");
            Console.WriteLine("6. Calcular altura del árbol");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese un nombre: ");
                    string? nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        arbol.Insertar(nombre);
                        Console.WriteLine($"Nombre '{nombre}' insertado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Debe ingresar un nombre válido.");
                    }
                    break;

                case 2:
                    arbol.PreOrden();
                    break;

                case 3:
                    arbol.InOrden();
                    break;

                case 4:
                    arbol.PostOrden();
                    break;

                case 5:
                    Console.Write("Ingrese el nombre a buscar: ");
                    string? nombreBuscar = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nombreBuscar))
                    {
                        Console.WriteLine("Entrada inválida. Debe ingresar un nombre válido.");
                    }
                    else
                    {
                        if (arbol.Buscar(nombreBuscar))
                        {
                            Console.WriteLine($"El nombre '{nombreBuscar}' SÍ está en el árbol.");
                        }
                        else
                        {
                            Console.WriteLine($"El nombre '{nombreBuscar}' NO está en el árbol.");
                        }
                    }
                    break;

                case 6:
                    int altura = arbol.CalcularAltura();
                    Console.WriteLine($"La altura del árbol es: {altura}");
                    break;

                case 7:
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        } while (opcion != 7);
    }
}
using System;
using System.Collections.Generic;

namespace PremiacionDeportistas
{
    // Clase para almacenar la información de un deportista
    class Deportista
    {
        // Propiedades del deportista: nombre, puntaje y disciplina
        public string Nombre { get; set; }
        public int Puntaje { get; set; }
        public string Disciplina { get; set; }

        // Constructor de la clase Deportista
        public Deportista(string nombre, int puntaje, string disciplina)
        {
            // Validar que el nombre y la disciplina no sean nulos o vacíos
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Puntaje = puntaje;
            Disciplina = disciplina ?? throw new ArgumentNullException(nameof(disciplina));
        }
    }

    // Clase que representa un nodo del árbol binario de búsqueda
    class NodoArbol
    {
        // Propiedades del nodo: datos del deportista y referencias a hijos
        public Deportista Datos { get; set; }
        public NodoArbol? Izquierda { get; set; }  // Hijo izquierdo, puede ser nulo
        public NodoArbol? Derecha { get; set; }    // Hijo derecho, puede ser nulo

        // Constructor del nodo
        public NodoArbol(Deportista datos)
        {
            // Validar que los datos no sean nulos
            Datos = datos ?? throw new ArgumentNullException(nameof(datos));
            Izquierda = null;  // Inicializamos los hijos como nulos
            Derecha = null;
        }
    }

    // Clase que implementa el árbol binario de búsqueda para gestionar deportistas
    class ArbolBinario
    {
        private NodoArbol? raiz;  // Raíz del árbol, puede ser nula
        private Dictionary<string, List<Deportista>> disciplinaDeportistas;  // Diccionario para agrupar deportistas por disciplina

        // Constructor del árbol binario
        public ArbolBinario()
        {
            raiz = null;  // Inicializamos la raíz como nula
            disciplinaDeportistas = new Dictionary<string, List<Deportista>>();  // Inicializamos el diccionario
        }

        // Método público para agregar un deportista al árbol
        public void Agregar(string? nombre, int puntaje, string? disciplina)
        {
            // Validar que el nombre y la disciplina no estén vacíos o sean nulos
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(disciplina))
                throw new ArgumentException("La disciplina no puede estar vacía.", nameof(disciplina));

            // Crear un nuevo deportista con los datos ingresados
            Deportista deportista = new Deportista(nombre, puntaje, disciplina);
            // Insertar el deportista en el árbol
            raiz = Insertar(raiz, deportista);

            // Agregar el deportista al diccionario por disciplina
            if (!disciplinaDeportistas.ContainsKey(disciplina))
            {
                disciplinaDeportistas[disciplina] = new List<Deportista>();
            }
            disciplinaDeportistas[disciplina].Add(deportista);
        }

        // Método privado para insertar un deportista en el árbol (recursivo)
        private NodoArbol? Insertar(NodoArbol? nodo, Deportista deportista)
        {
            // Si el nodo actual es nulo, creamos un nuevo nodo
            if (nodo == null)
            {
                return new NodoArbol(deportista);
            }

            // Comparar nombres para decidir dónde insertar (izquierda o derecha)
            if (string.Compare(deportista.Nombre, nodo.Datos.Nombre) < 0)
            {
                nodo.Izquierda = Insertar(nodo.Izquierda, deportista);
            }
            else if (string.Compare(deportista.Nombre, nodo.Datos.Nombre) > 0)
            {
                nodo.Derecha = Insertar(nodo.Derecha, deportista);
            }
            return nodo;
        }

        // Método para generar la reportería (muestra todos los deportistas y por disciplina)
        public void Reporteria()
        {
            // Mostrar todos los deportistas en orden alfabético
            Console.WriteLine("Todos los deportistas (orden alfabético):");
            RecorrerEnOrden(raiz);

            // Mostrar deportistas agrupados por disciplina
            Console.WriteLine("\nPor disciplina:");
            foreach (var disciplina in disciplinaDeportistas.Keys)
            {
                Console.WriteLine($"\nDisciplina: {disciplina}");
                ConsultarGanadores(disciplina);
            }
        }

        // Método privado para recorrer el árbol en orden (in-order traversal)
        private void RecorrerEnOrden(NodoArbol? nodo)
        {
            if (nodo != null)
            {
                // Recorrer subárbol izquierdo
                RecorrerEnOrden(nodo.Izquierda);
                // Mostrar datos del nodo actual
                Console.WriteLine($"{nodo.Datos.Nombre}: {nodo.Datos.Puntaje} puntos ({nodo.Datos.Disciplina})");
                // Recorrer subárbol derecho
                RecorrerEnOrden(nodo.Derecha);
            }
        }

        // Método para consultar los deportistas de una disciplina específica
        public void ConsultarGanadores(string disciplina)
        {
            // Validar que la disciplina no sea nula
            if (disciplina == null)
                throw new ArgumentNullException(nameof(disciplina));

            // Verificar si la disciplina existe en el diccionario
            if (disciplinaDeportistas.ContainsKey(disciplina))
            {
                // Mostrar cada deportista de la disciplina
                foreach (var deportista in disciplinaDeportistas[disciplina])
                {
                    Console.WriteLine($"{deportista.Nombre}: {deportista.Puntaje} puntos");
                }
            }
            else
            {
                Console.WriteLine("Disciplina no encontrada.");
            }
        }
    }

    // Clase principal del programa
    class Program
    {
        // Método principal que inicia la ejecución del programa
        static void Main(string[] args)
        {
            ArbolBinario arbol = new ArbolBinario();  // Crear una instancia del árbol
            bool continuar = true;  // Variable para controlar el bucle del menú

            // Bucle principal del programa
            while (continuar)
            {
                // Mostrar el menú de opciones
                Console.WriteLine("\n--- Menú ---");
                Console.WriteLine("1. Agregar deportista");
                Console.WriteLine("2. Mostrar reportería");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                string? opcion = Console.ReadLine();  // Leer la opción del usuario

                // Evaluar la opción seleccionada
                switch (opcion)
                {
                    case "1":
                        // Opción para agregar un deportista
                        Console.Write("Ingrese el nombre del deportista: ");
                        string? nombre = Console.ReadLine();
                        Console.Write("Ingrese el puntaje: ");
                        int puntaje;
                        // Validar que el puntaje sea un número positivo
                        while (!int.TryParse(Console.ReadLine(), out puntaje) || puntaje < 0)
                        {
                            Console.Write("Puntaje inválido. Ingrese un número positivo: ");
                        }
                        Console.Write("Ingrese la disciplina: ");
                        string? disciplina = Console.ReadLine();
                        arbol.Agregar(nombre, puntaje, disciplina);
                        Console.WriteLine("Deportista agregado con éxito.");
                        break;

                    case "2":
                        // Opción para mostrar la reportería
                        arbol.Reporteria();
                        break;

                    case "3":
                        // Opción para salir del programa
                        continuar = false;
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        // Opción inválida
                        Console.WriteLine("Opción inválida, intenta de nuevo.");
                        break;
                }
            }
        }
    }
}
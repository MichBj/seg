using System;
using System.Collections.Generic;

// Clase para representar un nodo (ciudad) en el grafo
class Node
{
    public string CityName { get; set; } // Nombre de la ciudad
    public List<(Node, int)> Neighbors { get; private set; } // Lista de vecinos (nodo, costo)

    public Node(string cityName)
    {
        CityName = cityName;
        Neighbors = new List<(Node, int)>();
    }

    // Añadir un vecino (conexión a otro nodo con un costo)
    public void AddNeighbor(Node neighbor, int cost)
    {
        Neighbors.Add((neighbor, cost));
        neighbor.Neighbors.Add((this, cost)); // Grafo no dirigido
    }
}

// Clase para representar el grafo
class Graph
{
    private List<Node> nodes; // Lista de nodos (ciudades)

    public Graph()
    {
        nodes = new List<Node>();
    }

    // Añadir un nodo al grafo
    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    // Obtener un nodo por su nombre
    public Node GetNodeByName(string cityName)
    {
        Node? node = nodes.Find(n => n.CityName == cityName);
        if (node == null)
        {
            throw new ArgumentException($"Ciudad '{cityName}' no encontrada en el grafo.");
        }
        return node; // El compilador ahora sabe que node no es null
    }

    // Algoritmo de Dijkstra para encontrar la ruta más barata
    public void Dijkstra(string startCity, string endCity)
    {
        Node start;
        Node end;

        try
        {
            start = GetNodeByName(startCity);
            end = GetNodeByName(endCity);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Dictionary<Node, int> dist = new Dictionary<Node, int>(); // Distancias mínimas
        Dictionary<Node, Node?> parent = new Dictionary<Node, Node?>(); // Para reconstruir el camino
        PriorityQueue<(Node, int)> pq = new PriorityQueue<(Node, int)>((a, b) => a.Item2.CompareTo(b.Item2)); // Cola de prioridad
        HashSet<Node> visited = new HashSet<Node>(); // Nodos visitados

        // Inicializar distancias
        foreach (var node in nodes)
        {
            dist[node] = int.MaxValue;
            parent[node] = null;
        }

        dist[start] = 0;
        pq.Enqueue((start, 0));

        while (pq.Count > 0)
        {
            var (current, d) = pq.Dequeue();
            if (visited.Contains(current)) continue;

            visited.Add(current);

            foreach (var (neighbor, weight) in current.Neighbors)
            {
                if (!visited.Contains(neighbor) && dist[current] != int.MaxValue && dist[current] + weight < dist[neighbor])
                {
                    dist[neighbor] = dist[current] + weight;
                    parent[neighbor] = current;
                    pq.Enqueue((neighbor, dist[neighbor]));
                }
            }
        }

        // Reconstruir y mostrar el camino
        if (dist[end] == int.MaxValue)
        {
            Console.WriteLine("No hay ruta disponible entre estas ciudades.");
            return;
        }

        List<Node> path = new List<Node>();
        for (Node? at = end; at != null; at = parent[at]) // Cambiamos Node a Node? para permitir null
        {
            path.Add(at);
        }
        path.Reverse();

        Console.WriteLine("\nRuta más barata encontrada:");
        for (int i = 0; i < path.Count - 1; i++)
        {
            Node from = path[i];
            Node to = path[i + 1];
            int cost = from.Neighbors.Find(n => n.Item1 == to).Item2;
            Console.WriteLine($"{from.CityName} -> {to.CityName}: ${cost}");
        }
        Console.WriteLine($"Costo total: ${dist[end]}");
    }

    // Mostrar ciudades disponibles y devolver la lista de nombres
    public List<string> ShowCities()
    {
        List<string> cityNames = new List<string>();
        Console.WriteLine("Ciudades disponibles:");
        for (int i = 0; i < nodes.Count; i++)
        {
            Console.WriteLine($"{i}: {nodes[i].CityName}");
            cityNames.Add(nodes[i].CityName);
        }
        return cityNames;
    }

    // Obtener el índice de un nodo
    public int GetNodeIndex(Node node)
    {
        return nodes.IndexOf(node);
    }
}

// Clase para simular una base de datos de vuelos
class FlightDatabase
{
    public List<(string from, string to, int cost)> Flights { get; private set; }

    public FlightDatabase()
    {
        Flights = new List<(string, string, int)>
        {
            ("New York", "Los Angeles", 400),
            ("New York", "Chicago", 200),
            ("Los Angeles", "Chicago", 300),
            ("Chicago", "Miami", 250),
            ("Los Angeles", "Miami", 500),
            ("Seattle", "San Francisco", 150),
            ("Seattle", "Denver", 200),
            ("San Francisco", "Houston", 350),
            ("Denver", "Houston", 300),
            ("Denver", "Boston", 400),
            ("Houston", "Boston", 250)
        };
    }
}

// Implementación simple de una cola de prioridad
class PriorityQueue<T>
{
    private List<T> list;
    private Comparison<T> comparison;

    public PriorityQueue(Comparison<T> comparison)
    {
        this.list = new List<T>();
        this.comparison = comparison;
    }

    public void Enqueue(T item)
    {
        list.Add(item);
        list.Sort(comparison);
    }

    public T Dequeue()
    {
        if (list.Count == 0) throw new InvalidOperationException("Queue is empty");
        T item = list[0];
        list.RemoveAt(0);
        return item;
    }

    public int Count => list.Count;
}

class Program
{
    static void Main(string[] args)
    {
        // Cargar la "base de datos" de vuelos
        FlightDatabase db = new FlightDatabase();
        Graph g = new Graph();

        // Crear nodos para cada ciudad única
        HashSet<string> uniqueCities = new HashSet<string>();
        foreach (var flight in db.Flights)
        {
            uniqueCities.Add(flight.from);
            uniqueCities.Add(flight.to);
        }

        // Añadir nodos al grafo
        foreach (var city in uniqueCities)
        {
            g.AddNode(new Node(city));
        }

        // Añadir conexiones desde la base de datos
        foreach (var flight in db.Flights)
        {
            Node fromNode = g.GetNodeByName(flight.from);
            Node toNode = g.GetNodeByName(flight.to);
            fromNode.AddNeighbor(toNode, flight.cost);
        }

        Console.WriteLine("Sistema de búsqueda de vuelos baratos");
        List<string> cityNames = g.ShowCities();

        // Búsqueda manual
        while (true)
        {
            Console.Write("\nIngrese ciudad de origen (número) o -1 para salir: ");
            string? startInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(startInput))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                continue;
            }

            if (!int.TryParse(startInput, out int startIndex))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                continue;
            }

            if (startIndex == -1) break;

            Console.Write("Ingrese ciudad de destino (número): ");
            string? endInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(endInput))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                continue;
            }

            if (!int.TryParse(endInput, out int endIndex))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                continue;
            }

            if (startIndex < 0 || startIndex >= uniqueCities.Count || endIndex < 0 || endIndex >= uniqueCities.Count)
            {
                Console.WriteLine($"Entrada inválida. Use números entre 0 y {uniqueCities.Count - 1}.");
                continue;
            }

            // Obtener los nombres de las ciudades a partir de los índices
            string startCity = cityNames[startIndex];
            string endCity = cityNames[endIndex];

            g.Dijkstra(startCity, endCity);
        }
    }
}
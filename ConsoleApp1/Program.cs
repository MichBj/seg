using System;

public class Node
{
    // Datos almacenados en el nodo
    public int Data;
    // Referencia al siguiente nodo en la lista
    public Node? Next;

    // Constructor del nodo que inicializa los datos y establece el siguiente nodo como null
    public Node(int data)
    {
        this.Data = data;
        this.Next = null;
    }

    // Método para obtener los datos del nodo
    public int GetData()
    {
        return Data;
    }

    // Método para establecer los datos del nodo
    public void SetData(int data)
    {
        this.Data = data;
    }

    // Método para obtener la referencia al siguiente nodo
    public Node? GetNext()
    {
        return Next;
    }

    // Método para establecer la referencia al siguiente nodo
    public void SetNext(Node? next)
    {
        this.Next = next;
    }
}

public class LinkedList
{
    // Referencia al primer nodo de la lista (cabeza)
    private Node? head;

    // Constructor de la lista enlazada que inicializa la cabeza como null
    public LinkedList()
    {
        head = null;
    }

    // Método para añadir un nuevo nodo al final de la lista
    public void Append(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            // Si la lista está vacía, el nuevo nodo se convierte en la cabeza
            head = newNode;
            return;
        }
        Node current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        // Añadimos el nuevo nodo al final
        current.Next = newNode;
    }

    // Método para eliminar nodos que están fuera del rango especificado
    public void RemoveNodesOutsideRange(int lowerBound, int upperBound)
    {
        // Eliminamos nodos desde el inicio hasta que encontremos uno dentro del rango
        while (head != null && (head.Data < lowerBound || head.Data > upperBound))
        {
            head = head.Next;
        }

        if (head == null) return;

        // Ahora revisamos el resto de la lista
        Node current = head;
        while (current.Next != null)
        {
            if (current.Next.Data < lowerBound || current.Next.Data > upperBound)
            {
                // Si el siguiente nodo está fuera del rango, lo saltamos
                current.Next = current.Next.Next;
            }
            else
            {
                // Si no, avanzamos al siguiente nodo
                current = current.Next;
            }
        }
    }

    // Método para obtener la longitud de la lista
    public int GetLength()
    {
        int count = 0;
        Node? current = head;
        // Recorremos la lista contando los nodos
        while (current != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }

    // Sobrescribe el método ToString para devolver una representación en cadena de la lista
    public override string ToString()
    {
        string result = "";
        Node? current = head;
        // Construimos una cadena con los datos de cada nodo
        while (current != null)
        {
            result += current.Data + " ";
            current = current.Next;
        }
        return result.Trim(); // Eliminamos el espacio extra al final
    }
}

public class Program
{
    // Método principal de la aplicación
    public static void Main(string[] args)
    {
        LinkedList list = new LinkedList();
        Random rand = new Random();

        // Generamos 50 números aleatorios del 1 al 999 y los añadimos a la lista
        for (int i = 0; i < 50; i++)
        {
            int randomNumber = rand.Next(1, 1000); // Next(1, 1000) genera números de 1 a 999
            list.Append(randomNumber);
        }

        Console.WriteLine("Lista original:");
        Console.WriteLine(list.ToString());

        // Leemos el rango de valores desde el teclado
        Console.WriteLine("Introduce el límite inferior del rango:");
        string? lowerBoundInput = Console.ReadLine();
        if (!int.TryParse(lowerBoundInput, out int lowerBound))
        {
            Console.WriteLine("Entrada no válida para el límite inferior. Usando 0 por defecto.");
            lowerBound = 0;
        }

        Console.WriteLine("Introduce el límite superior del rango:");
        string? upperBoundInput = Console.ReadLine();
        if (!int.TryParse(upperBoundInput, out int upperBound))
        {
            Console.WriteLine("Entrada no válida para el límite superior. Usando 1000 por defecto.");
            upperBound = 1000;
        }

        // Eliminamos los nodos fuera del rango especificado
        list.RemoveNodesOutsideRange(lowerBound, upperBound);

        Console.WriteLine("Lista después de eliminar nodos fuera del rango:");
        Console.WriteLine(list.ToString());

        // Añadimos algunos elementos a la lista para probar GetLength y ToString
        list.Append(1);
        list.Append(2);
        list.Append(3);

        // Imprimimos la longitud de la lista
        Console.WriteLine("Longitud de la lista: " + list.GetLength());
        // Imprimimos el contenido de la lista
        Console.WriteLine("Contenido de la lista: " + list.ToString());
    }
}
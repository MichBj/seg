using System;
using System.Collections.Generic;

class AsignaciondeAsientos
{
    static void Main()
    {
        // Aquí se simula la fila de espera usando una Queue
        Queue<string> colaPersonas = new Queue<string>();
        
        // Personas iniciales en espera
        colaPersonas.Enqueue("Juan");
        colaPersonas.Enqueue("María");
        colaPersonas.Enqueue("Pedro");
        colaPersonas.Enqueue("Ana");
        
        // Número total de asientos disponibles
        int asientosDisponibles = 30;
        
        // Lista para almacenar los asientos asignados
        List<string> asientosAsignados = new List<string>();
        
        // Bucle principal para la atracción
        while (asientosDisponibles > 0)
        {
            // Si no hay nadie en la cola y quedan asientos, solicitar más personas
            if (colaPersonas.Count == 0)
            {
                // Imprimimos un mensaje para agregar más gente hasta llegar a 30
                Console.WriteLine("No hay nadie en la cola. ¿Deseas añadir más personas? (s/n)");
                string? respuesta = Console.ReadLine();
                if (respuesta?.ToLower() == "s")
                {
                    // Introducimos a más gente a la fila
                    Console.WriteLine("Introduce el nombre de la nueva persona:");
                    string? nuevaPersona = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevaPersona))
                    {
                        colaPersonas.Enqueue(nuevaPersona);
                        Console.WriteLine($"{nuevaPersona} ha sido añadido/a a la cola.");
                    }
                    else
                    {
                        Console.WriteLine("No se puede añadir una persona nula a la cola.");
                    }
                }
                else
                {
                    Console.WriteLine("No se añadirán más personas. Fin de la simulación.");
                    break;
                }
            }
            else
            {
                // Ver quién está primero en la cola sin sacarlo
                string persona = colaPersonas.Peek();
                Console.WriteLine($"Siguiente persona en la cola: {persona}");
                
                // Sacar a la persona de la cola y asignarle un asiento
                colaPersonas.Dequeue();
                Console.WriteLine($"{persona} ha subido a la atracción.");
                
                // Añadir la persona a la lista de asientos asignados
                asientosAsignados.Add(persona);
                
                // Decrementar el número de asientos disponibles
                asientosDisponibles--;
            }
        }
        
        // Informar si se han vendido todos los asientos
        if (asientosDisponibles == 0)
        {
            Console.WriteLine("Todos los asientos han sido vendidos.");
        }
        else
        {
            Console.WriteLine($"Quedan {asientosDisponibles} asientos disponibles.");
        }
        
        // Mostrar los asientos asignados
        Console.WriteLine("\nAsientos asignados:");
        for (int i = 0; i < asientosAsignados.Count; i++)
        {
            Console.WriteLine($"Asiento {i + 1}: {asientosAsignados[i]}");
        }
    }
}
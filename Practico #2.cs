using System;
using System.Collections.Generic;

class AsignaciondeAsientos
{
    static void Main()
    {
        //Aqui se simula la fila de espera
        Stack<string> colaPersonas = new Stack<string>();
        
        //Personas iniciales en espera
        colaPersonas.Push("Juan");
        colaPersonas.Push("María");
        colaPersonas.Push("Pedro");
        colaPersonas.Push("Ana");
        
        // Número total de asientos disponibles
        int asientosDisponibles = 30;
        
        // Bucle principal para la atracción
        while (asientosDisponibles > 0)
        {
            // Si no hay nadie en la cola y quedan asientos, solicitar más personas
            if (colaPersonas.Count == 0)
            {
                //Imprimimos un mensaje para agregara mas Gente hasta llegar a 30
                Console.WriteLine("No hay nadie en la cola. ¿Deseas añadir más personas? (s/n)");
                string? respuesta = Console.ReadLine();
                if (respuesta?.ToLower() == "s")
                {
                    //introducimos a mas gente a la fila
                    Console.WriteLine("Introduce el nombre de la nueva persona:");
                    string? nuevaPersona = Console.ReadLine();
                    if (nuevaPersona != null)
                    {
                        colaPersonas.Push(nuevaPersona);
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
                string? persona = colaPersonas.Peek();
                if (persona != null)
                {
                    Console.WriteLine($"Siguiente persona en la cola: {persona}");
                
                    // Sacar a la persona de la cola y asignarle un asiento
                    colaPersonas.Pop();
                    Console.WriteLine($"{persona} ha subido a la atracción.");
                
                    // Decrementar el número de asientos disponibles
                    asientosDisponibles--;
                }
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
    }
}
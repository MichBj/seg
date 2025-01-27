using System;
using System.Collections.Generic;

class Program
{
    static void Hanoi(int n, Stack<int> source, Stack<int> target, Stack<int> auxiliary)
    {
        if (n > 0)
        {
            // Mueve n - 1 discos de la fuente a la auxiliar, usando el destino como intermediario
            Hanoi(n - 1, source, auxiliary, target);

            // Mueve el disco más grande de la fuente al destino
            target.Push(source.Pop());
            Console.WriteLine($"Mueve el disco {target.Peek()} de la torre {GetTowerName(source, source, auxiliary, target)} a la torre {GetTowerName(target, source, auxiliary, target)}");

            // Mueve n - 1 discos de la auxiliar al destino, usando la fuente como intermediario
            Hanoi(n - 1, auxiliary, target, source);
        }
    }

    static string GetTowerName(Stack<int> stack, Stack<int> source, Stack<int> auxiliary, Stack<int> target)
    {
        if (stack == source) return "A";
        if (stack == auxiliary) return "B";
        return "C";
    }

    static void Main(string[] args)
    {
        int n = 3; // Número de discos
        Stack<int> source = new Stack<int>();
        Stack<int> auxiliary = new Stack<int>();
        Stack<int> target = new Stack<int>();

        // Inicializamos la torre A con discos en orden descendente
        for (int i = n; i > 0; i--)
        {
            source.Push(i);
        }

        Hanoi(n, source, target, auxiliary);
    }
}
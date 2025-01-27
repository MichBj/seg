using System;
using System.Collections.Generic;

class Program
{
    static bool IsBalanced(string expression)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c); // Almacenamos el símbolo de apertura
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                // Si la pila está vacía o el símbolo de cierre no coincide con el último símbolo de apertura
                if (stack.Count == 0) return false;

                char lastOpen = stack.Pop();
                if ((c == ')' && lastOpen != '(') || 
                    (c == ']' && lastOpen != '[') || 
                    (c == '}' && lastOpen != '{'))
                {
                    return false;
                }
            }
        }

        // La expresión está balanceada si la pila está vacía al final
        return stack.Count == 0;
    }

    static void Main(string[] args)
    {
        string expresion = "{7+(8*5)-[(9-7)+(4+1)]}";
        bool resultado = IsBalanced(expresion);
        Console.WriteLine("La formula {0} está {1}balanceada.", expresion, resultado ? "" : "no ");
    }
}
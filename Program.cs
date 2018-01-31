using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            Exercice1();
        }

        private static void Exercice1()
        {
            var numbers = new List<int>();

            DisplayCommands();

            var input = Console.ReadLine();
            while (input != "*" && numbers.Count <= 20)
            {
                var result = int.TryParse(input, out var number);
                if (result)
                {
                    numbers.Add(number);
                }
                else
                {
                    switch (input)
                    {
                        case "+":
                            numbers.Sort();
                            DisplayList(numbers);
                            break;
                        case "-":
                            numbers = numbers.OrderByDescending(i => i).ToList();
                            DisplayList(numbers);
                            break;
                        case "=":
                            Console.WriteLine(numbers.Sum());
                            break;
                        default:
                            Console.Error.WriteLine("Invalid input!");
                            break;
                    }
                }

                if (numbers.Count <= 20)
                {
                    input = Console.ReadLine();
                }
            }
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("Please input your 20 or less numbers.");
            Console.WriteLine("Commands are:");
            Console.WriteLine("    + : Sort the list, then display it");
            Console.WriteLine("    - : Sort the list in descending order, then display it");
            Console.WriteLine("    = : Display a sum of the list");
            Console.WriteLine("    * : Quit");
        }

        private static void DisplayList(List<int> list)
        {
            Console.WriteLine(string.Join(", ", list.ToArray()));
        }
    }
}
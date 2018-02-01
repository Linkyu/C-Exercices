using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Exercice6();
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

        ////////////////////////////////////////////////////

        private static void Exercice2()
        {
            var expression = Console.ReadLine();
            try
            {
                CheckExpression(expression);
                var expressions = new Expression(expression);
            }
            catch (InvalidAbstractExpressionException invalidAbstractExpressionException)
            {
                Console.WriteLine(invalidAbstractExpressionException);
                throw;
            }
        }

        private static void CheckExpression(string expression)
        {
            CheckValidCharacters(expression);
            //CheckValidConstruction(expression);
        }

        private static void CheckValidConstruction(string expression)
        {
            throw new NotImplementedException();
        }

        private static void CheckValidCharacters(string expression)
        {
            var authorizedCharacters = new List<char> {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '+'};
            foreach (var character in expression)
            {
                if (!authorizedCharacters.Contains(character))
                {
                    throw new InvalidAbstractExpressionException(character);
                }
            }
        }

        private class InvalidAbstractExpressionException : Exception
        {
            public InvalidAbstractExpressionException(char character)
            {
                Console.Error.WriteLine("Invalid character: " + character);
            }
        }

        private class Expression
        {
            private List<ExpressionToken> Tokens { get; set; }

            public Expression(string expression)
            {
                Tokens = new List<ExpressionToken>();
                var sub = string.Empty;
                
                foreach (var character in expression)
                {
                    if (int.TryParse(""+character, out var garbage))
                    {
                        sub += character;
                    }
                    else
                    {
                        int.TryParse(sub, out var operand);
                        Tokens.Add(new Operand(operand));
                        Tokens.Add(new Operator(character));
                    }
                }
            }

            private Operand Calculate(int id)
            {
                var operand1 = (Operand) Tokens[id - 1];
                var operand2 = (Operand) Tokens[id + 1];
                var operatorToken = (Operator) Tokens[id];

                return operatorToken.Operate(operand1, operand2);
            }

            public int Evaluate()
            {
                // TODO: this
                throw new NotImplementedException();
            }
        }
        
        private class ExpressionToken
        {
            
        }

        private class Operator : ExpressionToken
        {
            private char Operation { get; set; }
            
            public Operator(char character)
            {
                Operation = character;
            }

            public Operand Operate(Operand operand1, Operand operand2)
            {
                switch (Operation)
                {
                        case '+':
                            return Add(operand1, operand2);
                        case '-':
                            return Substract(operand1, operand2);
                        default:
                            throw new Exception();
                }
            }

            private static Operand Add(Operand operand1, Operand operand2)
            {
                return operand1 + operand2;
            }

            private static Operand Substract(Operand operand1, Operand operand2)
            {
                return operand2 - operand1;
            }
        }

        private class Operand : ExpressionToken
        {
            public int Value { get; private set; }
            
            public Operand(int operand)
            {
                Value = operand;
            }

            public static Operand operator +(Operand o1, Operand o2) => new Operand(o1.Value + o2.Value);
            public static Operand operator -(Operand o1, Operand o2) => new Operand(o1.Value - o2.Value);
        }

        ////////////////////////////////////////////////////

        private static void Exercice3()
        {
            const int start = 100;
            const int finish = 500;

            for (var current = start; current <= finish; current++)
            {
                if (IsArmstrong(current))
                {
                    Console.WriteLine(current);
                }
            }
            
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        private static bool IsArmstrong(int number)
        {
            double sum = 0;
            foreach (var c in number.ToString())
            {
                int.TryParse(new string(c, 1), out var cube);
                sum += Math.Pow(cube, 3);
            }

            return Math.Abs(sum - number) < 0.1;
        }

        ////////////////////////////////////////////////////

        private static void Exercice4()
        {
            const int minimum = 1;
            const int maximum = 100;
            var target=new Random().Next(1,100);
            int input;
            var playerMin = minimum;
            var playerMax = maximum;

            do
            {
                Console.WriteLine("Please enter a valid number (" + minimum + " - " + maximum + "): ");
                var inputValid = int.TryParse(Console.ReadLine(), out input);
                if (inputValid)
                {
                    if (input < target)
                    {
                        Console.WriteLine("Higher!");
                        if (input > playerMin)
                        {
                            playerMin = input;
                        }

                        DisplayProgressBar(minimum, maximum, playerMin, playerMax);
                    }
                    else if (input > target)
                    {
                        Console.WriteLine("Lower!");
                        if (input < playerMax)
                        {
                            playerMax = input;
                        }

                        DisplayProgressBar(minimum, maximum, playerMin, playerMax);
                    }
                    else
                    {
                        Console.WriteLine("yayyy.");
                        Thread.Sleep(5000);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Invalid number, please try again");
                }
            } while (input != target);
        }

        private static void DisplayProgressBar(int min, int max, int playerMin, int playerMax)
        {
            Console.Write( "[");
            for (var i = min; i <= playerMin; ++i) {
                Console.Write("_");
            }
            for (var i = playerMin + 1; i < playerMax; ++i) {
                Console.Write("#");
            }
            for (var i = playerMax + 1; i <= max; ++i) {
                Console.Write("_");
            }
            Console.WriteLine("]");
        }

        ////////////////////////////////////////////////////

        private static void Exercice5()
        {
            var input = Console.ReadLine();
            if (input == null) return;
            
            var dict = new Dictionary<char, int>();

            foreach (var c in input)
            {
                var key = char.ToLower(c);
                if (dict.ContainsKey(key))
                {
                    dict[key]++;
                }
                else
                {
                    dict[key] = 1;
                }
            }

            foreach (var item in dict)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
        }

        ////////////////////////////////////////////////////

        private List<Animal> animals;
        
        private static void Exercice6()
        {
            var input = string.Empty;

            do
            {
                input = Console.ReadLine();

                //Interpret(input);

            } while (input != "*");
        }
        
        private class AnimalLister
        {
            
        }

        private void Interpret(string input)
        {
            if (new List<string>{"dogs", "cats"}.Contains(input))
            {
                ListAll(input);
            }
            else
            {
                var split = input.Split(' ');
                if (split.Length != 3)
                {
                    Console.WriteLine("Incorrect parameters");
                }
                else
                {
                    int.TryParse(split[2], out var age);
                    CreateAnimal(split[0].ToLower(), split[1], age);
                }
            }
        }

        private void CreateAnimal(string type, string name, int age)
        {
            if (type == "dog")
            {
                animals.Add(new Dog(name, age));
            }
            else
            {
                animals.Add(new Cat(name, age));
            }
        }

        private void ListAll(string input)
        {
            var typeToList = input == "dogs" ? typeof(Dog) : typeof(Cat);

            Console.WriteLine("List of " + typeToList + "s: ");
            foreach (var animal in animals)
            {
                if (animal.GetType() == typeToList)
                {
                    Console.WriteLine(animal.name + ": " + animal.age + " years old");
                }
            }
        }

        class Animal
        {
            public string name { get; private set; }
            public int age { get; private set; }
            private string noise { get; set; }
            
            public Animal(string name, int age)
            {
                this.name = name;
                this.age = age;
                this.noise = GenerateNoise();
            }

            protected virtual string GenerateNoise()
            {
                throw new NotImplementedException();
            }

            public void Talk()
            {
                Console.WriteLine(noise  + " " + name);
            }
        }

        class Dog : Animal
        {
            public Dog(string name, int age) : base(name, age)
            {
            }

            protected override string GenerateNoise()
            {
                return "Woof";
            }
        }

        class Cat : Animal
        {
            public Cat(string name, int age) : base(name, age)
            {
            }

            protected override string GenerateNoise()
            {
                return "Meow";
            }
        }

        ////////////////////////////////////////////////////

        // Unfinished as well
        private static void Exercice6Simpler()
        {
            
            var input = string.Empty;

            var animalList = new List<Animal>();

            do
            {
                input = Console.ReadLine();
                if (input == null) continue;
                
                if (new List<string>{"dogs", "cats"}.Contains(input.ToLower()))
                {
                    ListAnimals(input, animalList);
                }
                else
                {
                    var split = input.Split(' ');
                    if (split.Length != 3)
                    {
                        Console.WriteLine("Incorrect parameters");
                    }
                    else
                    {
                        int.TryParse(split[2], out var age);
                        if (split[0].ToLower() == "dog")
                        {
                            animalList.Add(new Dog(split[1], age));
                        }
                        else
                        {
                            animalList.Add(new Cat(split[1], age));
                        }
                    }
                }

            } while (input != "*");
        }

        private static void ListAnimals(string input, List<Animal> animalList)
        {
            throw new NotImplementedException();
        }
        
        ////////////////////////////////////////////////////

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollatzConjecture
{
    internal class Program
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void Main(string[] args)
        {
            Console.Write("Type how many numbers do you want to be tested: ");
            int searchScope = int.Parse(Console.ReadLine());
            bool[] escapedTheDoom = new bool[searchScope];
            bool hasAnyNumberEscaped = false;
            int iterationNumber = 0;
            long currentNumber = 0;
            List<long> values = new List<long>();
            Dictionary<int, int> valuesIterations = new Dictionary<int, int>();
            bool looped = false;
            bool reachedOne = false;
            bool printFull = false;

            Console.WriteLine("");
            Console.WriteLine($"Collatz Conjecture for {searchScope} numbers");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            for (int i = 1; i <= searchScope; i++)
            {
                iterationNumber = 0;
                currentNumber = i;
                looped = false;
                reachedOne = false;
                values.Clear();
                values.Add(currentNumber);

                Console.WriteLine($"Current number to be tested: {currentNumber}");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("");

                while (!looped)
                {
                    iterationNumber++;
                    Console.WriteLine($"[{iterationNumber}]: {values[iterationNumber - 1]}");

                    if (currentNumber == 1 && reachedOne)
                    {
                        looped = true;
                        Console.WriteLine($"[{iterationNumber}]: Occurrence of 4-2-1 loop.");
                        break;
                    }

                    if (currentNumber == 1)
                    {
                        reachedOne = true;
                        Console.WriteLine($"[{iterationNumber}]: Reached 1");
                    }

                    if(currentNumber < 0)
                    {
                        Console.WriteLine("Number got under the natural numbers!!!");
                        Console.ReadLine();
                    }

                    if (currentNumber % 2 == 0)
                    {
                        currentNumber /= 2;
                        values.Add(currentNumber);
                    } else
                    {
                        currentNumber = currentNumber * 3 + 1;
                        values.Add(currentNumber);
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("----- Summary -----");

                valuesIterations.Add(i, iterationNumber - 4);

                if (looped)
                {
                    Console.WriteLine($"Number {i} ended after {valuesIterations[i]} iterations in 4-2-1 loop");
                    escapedTheDoom[i-1] = false;
                } else
                {
                    Console.WriteLine($"Number {i} DID NOT end after {valuesIterations[i]} iterations in 4-2-1 loop");
                    escapedTheDoom[i-1] = true;
                }

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("");

                if(!printFull)
                {
                    Console.WriteLine("Press any key to continue or F to print every number without the pause.");
                    ConsoleKey key = Console.ReadKey().Key;
                    Console.WriteLine("");
                    if(key == ConsoleKey.F)
                    {
                        printFull = true;
                    }

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                    Console.WriteLine("");
                    continue;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Global summarization");
            Console.WriteLine("--------------------");
            Console.WriteLine("");

            for (int i = 1; i <= valuesIterations.Count; i++)
            {
                Console.WriteLine($"Number {i} took: {valuesIterations[i]} iterations to end up in 4-2-1");
            }

            Console.WriteLine("");
            Console.WriteLine("Do you want to calculate which number took the most iterations? (This calculation is really expensive when going through a lot of numbers, expect heavy time consuption)");
            Console.WriteLine("Press C to continue or S to skip this");

            if(Console.ReadKey().Key == ConsoleKey.C)
            {
                Console.WriteLine("");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                Console.WriteLine("");
                int maxKey = valuesIterations.FirstOrDefault(x => x.Value == valuesIterations.Values.Max()).Key;
                Console.WriteLine($"The number {maxKey} had the biggest value of iterations: {valuesIterations.Values.Max()}");
            }

            Console.WriteLine("");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            Console.WriteLine("");

            int escapist = 0;
            for (int i = 0; i < escapedTheDoom.Length; i++)
            {
                if(escapedTheDoom[i])
                {
                    hasAnyNumberEscaped = true;
                    escapist = i;
                }
            }

            Console.WriteLine(hasAnyNumberEscaped ? $"A {escapist} ESCAPED THE 4-2-1 loop" : "Not a single number could escape the 4-2-1 loop");

            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine($"End of Collatz Conjecture for {searchScope} numbers");

            Console.WriteLine("");
            Console.WriteLine("Press any key to close the application.");
            Console.ReadKey();
        }
    }
}

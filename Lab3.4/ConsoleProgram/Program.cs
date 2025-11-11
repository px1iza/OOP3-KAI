using System;
using ClassLibrary1;
using System.Linq;

namespace ConsoleProgram
{
    internal class Program
    {
        // Обробник події
        static void OnOverflowOccurred(object sender, OverflowEventArgs e)
        {
            Console.WriteLine($" Подія: {e.Message} (операнди: {e.Operand1}, {e.Operand2})");
        }

        // Делегат для обчислення суми елементів двовимірного масиву
        public delegate int SumDelegate(int[,] matrix);

        static void Main(string[] args)
        {
            // 🔸 1. Лямбда-вираз
            SumDelegate lambdaSum = matrix =>
            {
                int sum = 0;
                foreach (int value in matrix)
                    sum += value;
                return sum;
            };

            // 🔸 2. Анонімний метод
            SumDelegate anonSum = delegate (int[,] matrix)
            {
                int sum = 0;
                foreach (int value in matrix)
                    sum += value;
                return sum;
            };

            int[,] numbers = {
                { 10, 20, 30 },
                { 5, 15, 25 }
            };

            Console.WriteLine($"Сума (лямбда): {lambdaSum(numbers)}");
            Console.WriteLine($"Сума (анонімний): {anonSum(numbers)}");

            Arithmetic calc = new Arithmetic();
            calc.OverflowOccurred += OnOverflowOccurred!;

            Console.WriteLine("\nПеревірка переповнення:");
            int x = int.MaxValue;
            int y = 10;

            calc.Add(x, y);
            calc.Multiply(x, y);
        }
    }
}
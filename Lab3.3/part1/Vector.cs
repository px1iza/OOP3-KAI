using System.Text.Json;
using MemoryPack;
namespace MyApp
{
    [Serializable]
    [MemoryPackable]
    public partial class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Color { get; set; }

        [MemoryPackConstructor]
        public Vector() { }

        public Vector(double x, double y, string color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public void Increase(double factor)
        {
            X *= factor;
            Y *= factor;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Колір: {Color}");
            Console.WriteLine($"Координати кінця: ({X}, {Y})");
            Console.WriteLine($"Довжина вектора: {GetLength():F2}");
        }
    }

}

using System;
using System.Collections.Generic;

namespace VectorLibrary
{
    public class Vector : IComparable<Vector>
    {
        public double X { get; set; }
        public double Y { get; set; }

        // Властивість для обчислення довжини вектора
        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Збільшення вектора на певний відрізок (масштабування)
        public void Increase(double factor)
        {
            X *= factor;
            Y *= factor;
        }

        // Виведення інформації про вектор
        public string Output()
        {
            return $"Vector({X:F2}, {Y:F2}) - Length: {Length:F2}";
        }

        // Реалізація IComparable для сортування за довжиною
        public int CompareTo(Vector other)
        {
            if (other == null) return 1;
            return this.Length.CompareTo(other.Length);
        }

        public override string ToString()
        {
            return Output();
        }
    }
}
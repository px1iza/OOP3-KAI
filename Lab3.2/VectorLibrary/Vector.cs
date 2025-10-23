using System;
using System.Collections.Generic;

namespace VectorLibrary
{
    public class Vector : IComparable<Vector>
    {
        public double X { get; set; }
        public double Y { get; set; }

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

        public void Increase(double factor)
        {
            X *= factor;
            Y *= factor;
        }

        public string Output()
        {
            return $"Vector({X:F2}, {Y:F2}) - Length: {Length:F2}";
        }

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
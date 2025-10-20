using System;
using System.Collections.Generic;

namespace VectorLibrary
{
    public class VectorComparer : IComparer<Vector>
    {
        public int Compare(Vector x, Vector y)
        {
            if (x == null || y == null)
            {
                return x == null ? (y == null ? 0 : -1) : 1;
            }
            // Порівняння за координатою X
            return x.X.CompareTo(y.X);
        }
    }
}
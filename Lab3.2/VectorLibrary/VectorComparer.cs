using System;
using System.Collections.Generic;

namespace VectorLibrary
{
    public class VectorComparer : IComparer<Vector>
    {
        public int Compare(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
                return v1 == null ? (v2 == null ? 0 : -1) : 1;

            return v1.X.CompareTo(v2.X);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
namespace part1
{
    [Serializable]
    public class Vector : ISerializable
    {
        public string LineColor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Vector() { }

        public Vector(string lineColor, double x, double y)
        {
            LineColor = lineColor;
            X = x;
            Y = y;
        }

        // Конструктор для користувацької серіалізації
        protected Vector(SerializationInfo info, StreamingContext context)
        {
            LineColor = info.GetString("LineColor");
            X = info.GetDouble("X");
            Y = info.GetDouble("Y");
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public void Increase(double increment)
        {
            double length = GetLength();
            if (length > 0)
            {
                double factor = (length + increment) / length;
                X *= factor;
                Y *= factor;
            }
        }

        public string Output()
        {
            return $"Vector[Color={LineColor}, X={X:F2}, Y={Y:F2}, Length={GetLength():F2}]";
        }

        // Користувацька серіалізація
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LineColor", LineColor);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }
    }
}
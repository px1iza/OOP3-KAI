using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using MemoryPack;
namespace MyApp
{

    class Program
    {
        static void Main()
        {
            Vector v1 = new Vector(3.0, 4.0, "червоний");
            Vector v2 = new Vector(5.0, 9.0, "жовтий");
            Vector v3 = new Vector(2.5, 7.5, "Синій");

            Vector[] vectors = new Vector[] { v1, v2, v3 };

     
            string json = JsonSerializer.Serialize(vectors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("vectors.json", json);

           
            string loadedJson = File.ReadAllText("vectors.json");
            Vector[] loadedVectors = JsonSerializer.Deserialize<Vector[]>(loadedJson)!;

            List<Vector> vectorList = new List<Vector> { v1, v2, v3 };

            
            string jsonList = JsonSerializer.Serialize(vectorList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("vectorsList.json", jsonList);

            string loadedJsonList = File.ReadAllText("vectorsList.json");
            List<Vector> loadedVectorsList = JsonSerializer.Deserialize<List<Vector>>(loadedJsonList)!;

    
            XmlSerializer serializer = new XmlSerializer(typeof(List<Vector>));

            using (FileStream fs = new FileStream("vectors.xml", FileMode.Create))
            {
                serializer.Serialize(fs, vectorList);
            }

            Console.WriteLine("Список векторів збережено у vectors.xml");

            List<Vector> loadedVectorsListXml;
            using (FileStream fs = new FileStream("vectors.xml", FileMode.Open))
            {
                loadedVectorsListXml = (List<Vector>)serializer.Deserialize(fs)!;
            }

            byte[] mpBytes = MemoryPackSerializer.Serialize(vectorList);
            File.WriteAllBytes("vectors.bin", mpBytes);

            byte[] loadedMpBytes = File.ReadAllBytes("vectors.bin");
            List<Vector> loadedVectorsMp = MemoryPackSerializer.Deserialize<List<Vector>>(loadedMpBytes)!;

            Console.WriteLine("MemoryPack бінарна серіалізація виконана");

            using (var bw = new BinaryWriter(File.Open("vectors.txt", FileMode.Create)))
            {
                foreach (var v in vectors)
                {
                    bw.Write(v.X);
                    bw.Write(v.Y);
                    bw.Write(v.Color);
                }
            }
            using (var bw = new BinaryWriter(File.Open("vectors.bin", FileMode.Create)))
            {
                foreach (var v in vectors)
                {
                    bw.Write(v.X);
                    bw.Write(v.Y);
                    bw.Write(v.Color);
                }
            }

        }

    }
}
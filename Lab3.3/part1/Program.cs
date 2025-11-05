using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення масиву об'єктів
            Vector[] arr = new Vector[3];
            arr[0] = new Vector("Red", 3.5, 4.2);
            arr[1] = new Vector("Blue", 5.0, 12.0);
            arr[2] = new Vector("Green", 8.3, 6.4);

            Console.WriteLine(" Початковий масив");
            foreach (var item in arr)
                Console.WriteLine($"{item.Output()} (Довжина: {item.GetLength():F2})");

            // Демонстрація методу Increase
            Console.WriteLine("\n=== Збільшення вектора на 2.0 ===");
            arr[0].Increase(2.0);
            Console.WriteLine(arr[0].Output());

            // Бінарна серіалізація
            BinarySerialize(arr, "vectors_binary.dat");
            var restoredBin = BinaryDeserialize("vectors_binary.dat");
            Console.WriteLine("\n=== Відновлено з Binary ===");
            foreach (var item in restoredBin)
                Console.WriteLine(item.Output());

            // XML серіалізація
            XmlSerialize(arr, "vectors.xml");
            var restoredXml = XmlDeserialize("vectors.xml");
            Console.WriteLine("\n=== Відновлено з XML ===");
            foreach (var item in restoredXml)
                Console.WriteLine(item.Output());

            // JSON серіалізація
            JsonSerialize(arr, "vectors.json");
            var restoredJson = JsonDeserialize("vectors.json");
            Console.WriteLine("\n=== Відновлено з JSON ===");
            foreach (var item in restoredJson)
                Console.WriteLine(item.Output());

            // Користувацька серіалізація
            CustomSerialize(arr, "vectors_custom.txt");
            var restoredCustom = CustomDeserialize("vectors_custom.txt");
            Console.WriteLine("\n=== Відновлено з Custom ===");
            foreach (var item in restoredCustom)
                Console.WriteLine(item.Output());

            // Порівняння масиву та колекції
            List<Vector> list = new List<Vector>(arr);
            JsonSerializeList(list, "vectors_list.json");
            var restoredList = JsonDeserializeList("vectors_list.json");
            Console.WriteLine("\n=== Відновлено з JSON List ===");
            foreach (var item in restoredList)
                Console.WriteLine(item.Output());

            Console.WriteLine($"\n=== Порівняння ===");
            Console.WriteLine($"Масив: {arr.Length} елементів");
            Console.WriteLine($"Колекція: {list.Count} елементів");

            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        // Бінарна серіалізація
        static void BinarySerialize(Vector[] arr, string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                bf.Serialize(fs, arr);
            }
        }

        static Vector[] BinaryDeserialize(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                return (Vector[])bf.Deserialize(fs);
            }
        }

        // XML серіалізація
        static void XmlSerialize(Vector[] arr, string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Vector[]));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                xs.Serialize(fs, arr);
            }
        }

        static Vector[] XmlDeserialize(string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Vector[]));
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                return (Vector[])xs.Deserialize(fs);
            }
        }

        // JSON серіалізація (масив)
        static void JsonSerialize(Vector[] arr, string filename)
        {
            string json = JsonConvert.SerializeObject(arr, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        static Vector[] JsonDeserialize(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<Vector[]>(json);
        }

        // JSON серіалізація (колекція)
        static void JsonSerializeList(List<Vector> list, string filename)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        static List<Vector> JsonDeserializeList(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<List<Vector>>(json);
        }

        // Користувацька серіалізація
        static void CustomSerialize(Vector[] arr, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(arr.Length);
                foreach (var vector in arr)
                {
                    writer.WriteLine($"{vector.LineColor}|{vector.X}|{vector.Y}");
                }
            }
        }

        static Vector[] CustomDeserialize(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                int count = int.Parse(reader.ReadLine());
                Vector[] arr = new Vector[count];

                for (int i = 0; i < count; i++)
                {
                    string[] parts = reader.ReadLine().Split('|');
                    arr[i] = new Vector(parts[0], double.Parse(parts[1]), double.Parse(parts[2]));
                }
                return arr;
            }
        }
    }
}

using System.Text;
using DAL.Interfaces;


namespace DAL.DataProviders
{
    public class CustomProvider<T> : IDataProvider<T>
    {
        public string FileExtension => ".txt";

        public void Save(List<T> data, string fileName)
        {
            string path = $"../../../Results/{fileName}";
            using StreamWriter writer = new(path, false, Encoding.UTF8);
            foreach (var item in data)
            {
                writer.WriteLine(item?.ToString());
            }
            Console.WriteLine($"Custom text file saved to {path}");
        }

        public List<T> Load(string fileName)
        {
            string path = $"../../../Results/{fileName}";
            List<T> result = new();
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return result;
            }

            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (typeof(T) == typeof(string))
                {
                    result.Add((T)(object)line);
                }
                else
                {
                    Console.WriteLine($"Read line: {line}");
                }
            }
            return result;
        }
    }
}
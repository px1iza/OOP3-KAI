using System.Text.Json;
using DAL.Interfaces;

namespace DAL.DataProviders
{
    public class JSONProvider<T> : IDataProvider<T>
    {
        public string FileExtension => ".json";

        public void Save(List<T> data, string fileName)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText($"../../../Results/{fileName}", json);
        }

        public List<T> Load(string fileName)
        {
            if (!File.Exists($"../../../Results/{fileName}")) return new();
            string json = File.ReadAllText($"../../../Results/{fileName}");
            return JsonSerializer.Deserialize<List<T>>(json) ?? new();
        }
    }
}
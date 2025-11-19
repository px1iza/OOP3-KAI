using System.Text.Json;
using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.DataProvider
{

    public class JSONProvider<T> : IDataProvider<T>
    {
        public void Save(List<T> data, string fileName)
        {
            Directory.CreateDirectory("../../../Results");
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText($"../../../Results/{fileName}", json);
        }

        public List<T> Load(string fileName)
        {
            string path = $"../../../Results/{fileName}";
            if (!File.Exists(path)) return new();
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new();
        }
    }
}
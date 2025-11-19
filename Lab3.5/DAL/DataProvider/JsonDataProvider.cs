using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DAL.Interfaces;

namespace DAL.DataProvider
{
    public class JsonDataProvider<T> : IDataProvider<T>
    {
        private readonly string _filePath;

        public JsonDataProvider(string fileName)
        {
            string folder = "../../../Result/";
            Directory.CreateDirectory(folder);

            _filePath = Path.Combine(folder, fileName);

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public void Save(List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }

        public List<T> Load()
        {
            string json = File.ReadAllText(_filePath);

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}

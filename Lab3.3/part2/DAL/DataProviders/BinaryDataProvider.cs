using MemoryPack;
using DAL.Interfaces;

namespace DAL.DataProviders
{
    public class MemoryPackProvider<T> : IDataProvider<T>
    {
        public string FileExtension => ".bin";

        public void Save(List<T> data, string fileName)
        {
            byte[] bytes = MemoryPackSerializer.Serialize(data);
            File.WriteAllBytes($"../../../Results/{fileName}", bytes);
        }

        public List<T> Load(string fileName)
        {
            if (!File.Exists($"../../../Results/{fileName}")) return new();
            byte[] bytes = File.ReadAllBytes($"../../../Results/{fileName}");
            return MemoryPackSerializer.Deserialize<List<T>>(bytes) ?? new();
        }
    }
}
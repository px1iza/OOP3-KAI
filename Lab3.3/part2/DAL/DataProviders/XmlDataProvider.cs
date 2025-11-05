using System.Xml.Serialization;
using DAL.Interfaces;

namespace DAL.DataProviders
{
    public class XmlProvider<T> : IDataProvider<T>
    {
        public string FileExtension => ".xml";

        public void Save(List<T> data, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using var fs = File.Create($"../../../Results/{fileName}");
            serializer.Serialize(fs, data);
        }

        public List<T> Load(string fileName)
        {
            if (!File.Exists($"../../../Results/{fileName}")) return new();
            var serializer = new XmlSerializer(typeof(List<T>));
            using var fs = File.OpenRead($"../../../Results/{fileName}");
            return (List<T>?)serializer.Deserialize(fs) ?? new();
        }
    }
}
using DAL.Interfaces;

namespace DAL
{
    public class EntityContext<T>
    {
        private readonly IDataProvider<T> _provider;
        private readonly string _filePath;

        public EntityContext(IDataProvider<T> provider, string filePath)
        {
            _provider = provider;
            _filePath = filePath;
        }

        public void Save(List<T> items) => _provider.Save(items, _filePath);

        public List<T> Load() => _provider.Load(_filePath);
    }
}
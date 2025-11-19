using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDataProvider<T>
    {
        void Save(List<T> data, string fileName);
        List<T> Load(string fileName);
    }
}
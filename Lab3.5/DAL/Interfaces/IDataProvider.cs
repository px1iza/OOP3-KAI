using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDataProvider<T>
    {
        void Save(List<T> data);
        List<T> Load();
    }
}
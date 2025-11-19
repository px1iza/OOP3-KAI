using DAL.DataProvider;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public static class ServiceFactory
    {
        public static StudentService CreateStudentService()
        {
            IDataProvider<Student> provider = new JsonDataProvider<Student>("students.json");
            return new StudentService(provider);
        }
    }
}
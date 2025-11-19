using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class StudentService
    {
        private readonly IDataProvider<Student> _provider;

        public StudentService(IDataProvider<Student> provider)
        {
            _provider = provider;
        }

        public List<Student> GetAllStudents()
            => _provider.Load();

        public List<Student> GetStudentsWithIdealWeight()
        {
            return _provider.Load()
                .Where(s => s.Height - 110 == s.Weight)
                .ToList();
        }

        public void RegisterStudent(Student student)
        {
            if (!student.IsValidStudentID())
                throw new ArgumentException("Invalid student ID");

            if (student.Passport != null && !student.Passport.IsValidPassport())
                throw new ArgumentException("Invalid passport");

            var all = _provider.Load();
            all.Add(student);
            _provider.Save(all);
        }

        public void SaveStudents(List<Student> students)
        {
            _provider.Save(students);
        }
    }
}
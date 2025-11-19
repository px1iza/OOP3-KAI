using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class StudentService
    {
        private readonly IDataProvider<Student> _dataProvider;

        public StudentService(IDataProvider<Student> dataProvider)
        {
            _dataProvider = dataProvider;
        }


        public List<Student> GetAllStudents(string filePath)
        {
            return _dataProvider.Load(filePath);
        }

        public List<Student> GetStudentsWithIdealWeight(string filePath)
        {
            var students = _dataProvider.Load(filePath);

            return students
                .Where(s => s.Height - 110 == s.Weight)
                .ToList();
        }

        public void RegisterStudent(List<Student> currentList, Student newStudent, string filePath)
        {
            if (!newStudent.IsValidStudentID())
            {
                throw new ArgumentException("Invalid Student ID format.");
            }

            if (newStudent.Passport != null && !newStudent.Passport.IsValidPassport())
            {
                throw new ArgumentException("Invalid Passport format.");
            }

            currentList.Add(newStudent);
            _dataProvider.Save(currentList, filePath);
        }
    }
}